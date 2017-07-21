using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Store.Domain.Entities;
using Store.Domain.Concrete;
using Store.WebUI.Models;

namespace Store.WebUI.SearchLucene
{
    public class SearchProducts
    {
        private RAMDirectory _directory;

        public SearchProducts()
        {
            _directory = new RAMDirectory();
        }

        private void _addToLuceneIndex(Product sampleData, IndexWriter writer)
        {
            // remove older index entry
            var searchQuery = new TermQuery(new Term("Id", sampleData.ProductId.ToString()));
            writer.DeleteDocuments(searchQuery);

            // add new index entry
            var doc = new Document();

            // add lucene fields mapped to db fields
            doc.Add(new Field("ProductId", sampleData.ProductId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("Name", sampleData.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Description", sampleData.Description, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Price", sampleData.Price.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Picture", sampleData.Picture, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Category", sampleData.Category.Type, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("CategoryId", sampleData.Category.CategoryId.ToString(), Field.Store.YES, Field.Index.ANALYZED));


            // add entry to index
            writer.AddDocument(doc);
        }

        public void AddUpdateLuceneIndex(IEnumerable<Product> sampleDatas)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entry if any)
                foreach (var sampleData in sampleDatas) _addToLuceneIndex(sampleData, writer);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        public void ClearLuceneIndexRecord(int record_id)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // remove older index entry
                var searchQuery = new TermQuery(new Term("Id", record_id.ToString()));
                writer.DeleteDocuments(searchQuery);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        public bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
                using (var writer = new IndexWriter(_directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();

                    // close handles
                    analyzer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Optimize()
        {
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }

        private Product _mapLuceneDocumentToData(Document doc)
        {

            var productId = Convert.ToInt32(doc.Get("ProductId"));
            var name = doc.Get("Name");
            var description = doc.Get("Description");
            var picture = doc.Get("Picture");
            var price = Convert.ToSingle(doc.Get("Price"));
            Category category = new Category() { Type = doc.Get("Category"), CategoryId = Convert.ToInt32(doc.Get("CategoryId")) };
            return new Product
            {
                ProductId = productId,
                Name = name,
                Picture = picture,
                Description = description,
                Price = price,
                Category = category

            };
        }

        private IEnumerable<Product> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }

        private IEnumerable<Product> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }

        private static Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private IEnumerable<SearchProduct> _search(string searchQuery, string searchField = "")
        {
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", ""))) return new List<SearchProduct>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                var hits_limit = 1000;
                var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

                // search by single field
                if (!string.IsNullOrEmpty(searchField))
                {
                    var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, searchField, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search(query, hits_limit).ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    List<SearchProduct> products = new List<SearchProduct>();
                    using (var db = new ApplicationDbContext())
                    {
                        foreach (var elem in results)
                        {
                            products.Add(new SearchProduct(elem));
                        }
                    }
                    return products;
                }
                // search by multiple fields (ordered by RELEVANCE)
                else
                {
                    var parser = new MultiFieldQueryParser
                        (Lucene.Net.Util.Version.LUCENE_30, new[] { "ProductId", "Name", "Description" , "Price", "Category"}, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search(query, null, hits_limit, Sort.RELEVANCE).ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    List<SearchProduct> products = new List<SearchProduct>();
                    using (var db = new ApplicationDbContext())
                    {
                        foreach (var elem in results)
                        {
                            products.Add(new SearchProduct(elem));
                        }
                    }
                    return products;
                }
            }
        }

        public IEnumerable<SearchProduct> Search(string input, string fieldName = "")
        {
            if (string.IsNullOrEmpty(input)) return new List<SearchProduct>();

            var terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);

            return _search(input, fieldName);
        }

        public IEnumerable<SearchProduct> Search(string input)
        {
            if (string.IsNullOrEmpty(input)) return new List<SearchProduct>();

            return _search(input);
        }
    }

}