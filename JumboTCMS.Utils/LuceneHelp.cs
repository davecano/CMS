using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis;

namespace JumboTCMS.Utils.LuceneHelp
{
    ///E:/swf/ <summary>
    ///E:/swf/ 搜索内容
    ///E:/swf/ </summary>
    public class SearchItem
    {
        public string TableName
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 频道ID
        ///E:/swf/ </summary>
        public string ChannelId
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目ID
        ///E:/swf/ </summary>
        public string ClassId
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目名称
        ///E:/swf/ </summary>
        public string ClassName
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 标题
        ///E:/swf/ </summary>
        public string Title
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 链接地址
        ///E:/swf/ </summary>
        public string Url
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 缩略图
        ///E:/swf/ </summary>
        public string Img
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 简介
        ///E:/swf/ </summary>
        public string Summary
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 正文
        ///E:/swf/ </summary>
        public string Content
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 标签
        ///E:/swf/ </summary>
        public string Tags
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 更新日期
        ///E:/swf/ </summary>
        public string AddDate
        {
            get;
            set;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 更新年份
        ///E:/swf/ </summary>
        public string Year
        {
            get;
            set;
        }
    }
    public class SearchIndex
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 获得总数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="type"></param>
        ///E:/swf/ <param name="channelid"></param>
        ///E:/swf/ <param name="classid"></param>
        ///E:/swf/ <param name="year"></param>
        ///E:/swf/ <param name="keywords"></param>
        ///E:/swf/ <returns></returns>
        public static int GetCount(string type, string channelid, string classid, string year, String keywords, string groupname, out Dictionary<string, int> groupAggregate)
        {
            if (keywords.Length == 0)
            {
                keywords = "jUmBoT";
            }
            DateTime start = DateTime.Now;
            string[] types = type.Split(',');
            int _type_num = types.Length;
            IndexSearcher[] searchers = new IndexSearcher[_type_num];
            for (int i = 0; i < _type_num; i++)
            {
                searchers[i] = new IndexSearcher(HttpContext.Current.Server.MapPath("~/_data/index/" + types[i] + "/"));
            }
            MultiSearcher search = new MultiSearcher(searchers);

            BooleanQuery bq = new BooleanQuery();
            if (channelid != "0")
            {
                Term Term_channel = new Term("channelid", channelid);
                var termQuery1 = new TermQuery(Term_channel);
                bq.Add(termQuery1, BooleanClause.Occur.MUST);//添加到条件
            }
            if (JumboTCMS.Utils.Validator.StrToInt(year, 0) > 1900)
            {
                Term Term_year = new Term("year", year);
                var termQuery2 = new TermQuery(Term_year);
                bq.Add(termQuery2, BooleanClause.Occur.MUST);//添加到条件
            }
            string[] fields = { "title", "tags", "summary", "content", "fornull" };
            MultiFieldQueryParser parser = new MultiFieldQueryParser(fields, new StandardAnalyzer());//要查询的字段
            Query query = parser.Parse(keywords);
            bq.Add(query, BooleanClause.Occur.MUST);//添加到条件
            Hits hits = search.Search(bq);
            if (_type_num == 1)
            {
                groupAggregate = SimpleFacets.Facet(bq, searchers[0], groupname);
            }
            else
            {
                groupAggregate = null;
            }
            search.Close();
            return hits.Length();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 分布检索
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="type">多个用,隔开</param>
        ///E:/swf/ <param name="channelid"></param>
        ///E:/swf/ <param name="keywords">已经使用分词工具处理过</param>
        ///E:/swf/ <param name="pageLen"></param>
        ///E:/swf/ <param name="pageNo"></param>
        ///E:/swf/ <param name="recCount"></param>
        ///E:/swf/ <param name="eventTime"></param>
        ///E:/swf/ <returns></returns>
        public static List<SearchItem> Search(string type, string channelid, string classid, string year, String keywords, int pageLen, int pageNo, out int recCount, out double eventTime)
        {
            if (keywords.Length == 0)
            {
                keywords = "jUmBoT";
            }
            DateTime start = DateTime.Now;
            string[] types = type.Split(',');
            int _type_num = types.Length;
            IndexSearcher[] searchers = new IndexSearcher[_type_num];
            for (int i = 0; i < _type_num; i++)
            {
                searchers[i] = new IndexSearcher(HttpContext.Current.Server.MapPath("~/_data/index/" + types[i] + "/"));
            }
            MultiSearcher search = new MultiSearcher(searchers);

            BooleanQuery bq = new BooleanQuery();
            if (channelid != "0")
            {
                Term Term_channel = new Term("channelid", channelid);
                var termQuery1 = new TermQuery(Term_channel);
                bq.Add(termQuery1, BooleanClause.Occur.MUST);//添加到条件
            }
            if (JumboTCMS.Utils.Validator.StrToInt(year, 0) > 1900)
            {
                Term Term_year = new Term("year", year);
                var termQuery2 = new TermQuery(Term_year);
                bq.Add(termQuery2, BooleanClause.Occur.MUST);//添加到条件
            }
            string[] fields = { "title", "tags", "summary", "content", "fornull" };
            MultiFieldQueryParser parser = new MultiFieldQueryParser(fields, new StandardAnalyzer());//要查询的字段
            Query query = parser.Parse(keywords);
            bq.Add(query, BooleanClause.Occur.MUST);//添加到条件

            Sort sort = new Sort(new SortField(null, SortField.DOC, true)); //排序
            Hits hits = search.Search(bq, sort);
            //Hits hits = search.Search(bq, new Sort(SortField.FIELD_SCORE));
            //Hits hits = search.Search(bq, new Sort(new SortField[] { new SortField("title", SortField.SCORE, true), SortField.FIELD_SCORE }));
            List<SearchItem> result = new List<SearchItem>();
            recCount = hits.Length();
            //for (int i = 0; i < _type_num; i++)
            if (recCount > 0)
            {
                int i = (pageNo - 1) * pageLen;
                while (i < recCount && result.Count < pageLen)
                {
                    SearchItem news = null;
                    try
                    {
                        news = new SearchItem();
                        news.Id = hits.Doc(i).Get("id");
                        news.ChannelId = hits.Doc(i).Get("channelid");
                        news.ClassId = hits.Doc(i).Get("classid");
                        news.TableName = hits.Doc(i).Get("tablename");
                        news.Img = hits.Doc(i).Get("img");
                        news.Title = hits.Doc(i).Get("title");
                        news.Summary = hits.Doc(i).Get("summary");
                        news.Tags = hits.Doc(i).Get("tags");
                        news.Url = hits.Doc(i).Get("url");
                        news.AddDate = hits.Doc(i).Get("adddate");
                        news.Year = hits.Doc(i).Get("year");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        result.Add(news);
                        i++;
                    }
                }
                search.Close();
                TimeSpan duration = DateTime.Now - start;
                eventTime = (float)Convert.ToInt16(duration.TotalMilliseconds);
                return result;
            }
            else
            {
                search.Close();
                TimeSpan duration = DateTime.Now - start;
                eventTime = (float)Convert.ToInt16(duration.TotalMilliseconds);
                return null;
            }
        }
    }
    ///E:/swf/ <summary>
    ///E:/swf/ 简单分组
    ///E:/swf/ </summary>
    public class SimpleFacets
    {
        public static void Facet(BooleanQuery bq, IndexSearcher s, string field, Dictionary<string, int> dics)
        {
            StringIndex stringIndex = FieldCache_Fields.DEFAULT.GetStringIndex(s.GetIndexReader(), field);

            int[] c = new int[stringIndex.lookup.Length];

            FacetCollector results = new FacetCollector(c, stringIndex);

            s.Search(bq, results);

            DictionaryEntryQueue queue = new DictionaryEntryQueue(stringIndex.lookup.Length);

            for (int i = 1; i < stringIndex.lookup.Length; i++)
            {
                if (c[i] > 0 && stringIndex.lookup[i] != null && stringIndex.lookup[i] != "0")
                {
                    queue.Insert(new FacetEntry(stringIndex.lookup[i], -c[i]));
                }
            }
            int dictionaryEntrySize = queue.Size();
            for (int j = dictionaryEntrySize - 1; j >= 0; j--)
            {
                FacetEntry entry = queue.Pop() as FacetEntry;
                dics.Add(entry.Value, -entry.Count);
            }
        }
        public static Dictionary<string, int> Facet(Query query, IndexSearcher s, string field)
        {
            StringIndex stringIndex = FieldCache_Fields.DEFAULT.GetStringIndex(s.GetIndexReader(), field);

            int[] c = new int[stringIndex.lookup.Length];

            FacetCollector results = new FacetCollector(c, stringIndex);

            s.Search(query, results);

            DictionaryEntryQueue queue = new DictionaryEntryQueue(stringIndex.lookup.Length);

            for (int i = 1; i < stringIndex.lookup.Length; i++)
            {
                if (c[i] > 0 && stringIndex.lookup[i] != null && stringIndex.lookup[i] != "0")
                {
                    queue.Insert(new FacetEntry(stringIndex.lookup[i], -c[i]));
                }
            }
            int dictionaryEntrySize = queue.Size();
            Dictionary<string, int> dics = new Dictionary<string, int>();
            for (int j = dictionaryEntrySize - 1; j >= 0; j--)
            {
                FacetEntry entry = queue.Pop() as FacetEntry;
                dics.Add(entry.Value, -entry.Count);
            }
            return dics;
        }
        ///E:/swf/ <summary>Helper class for order the resulting array in value order 
        ///E:/swf/ </summary> 
        private sealed class DictionaryEntryQueue : Lucene.Net.Util.PriorityQueue
        {
            internal DictionaryEntryQueue(int size)
                : base()
            {
                Initialize(size);
            }

            public override bool LessThan(object a, object b)
            {
                FacetEntry de1 = (FacetEntry)a;
                FacetEntry de2 = (FacetEntry)b;
                return (int)de1.Count < (int)de2.Count;
            }
        }

        ///E:/swf/ <summary>class for work in the priority queue and avoid some boxing. 
        ///E:/swf/ </summary> 
        private class FacetEntry
        {
            // Fields 
            private int count;
            private string value;

            // Methods 
            public FacetEntry(string v, int c)
            {
                this.value = v;
                this.count = c;
            }

            public int Count
            {
                get { return count; }
                set { count = value; }
            }

            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

        }

        ///E:/swf/ <summary>collector that count the hits for every token 
        ///E:/swf/ </summary> 
        private class FacetCollector : HitCollector
        {
            // Fields 
            private int[] counter;
            private StringIndex si;

            // Methods 
            public FacetCollector(int[] c, StringIndex s)
            {
                this.counter = c;
                this.si = s;
            }

            public override void Collect(int doc, float score)
            {
                this.counter[this.si.order[doc]]++;
            }
        }
    }
}
