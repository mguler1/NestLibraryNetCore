using Nest;
using NestLibraryNetCore.Api.Models;
using System.Collections.Immutable;

namespace NestLibraryNetCore.Api.Repository
{
    public class ECommerceRepository
    {
        private readonly ElasticClient _client;
        private const string IndexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<IImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {
            //1.way
            //var result =await _client.SearchAsync<ECommerce>(s => s
            //    .Index(IndexName)
            //    .Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));

            //        foreach (var hit in result.Hits) 
            //        hit.Source.Id = hit.Id; // Assign the Id from the hit to the source object
            //        return result.Documents.ToImmutableList();

            //2.way
            var result = await _client.SearchAsync<ECommerce>(s => s
                .Index(IndexName)
                .Query(q => q.Term(t => t.CustomerFirstName.Suffix("keyword"), customerFirstName)));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id; // Assign the Id from the hit to the source object
            return result.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameList)
        {
            var termsQuery = new TermsQuery
            {
                Field = "customer_first_name.keyword",
                Terms = customerFirstNameList
            };
            var result = await _client.SearchAsync<ECommerce>(s => s
                .Index(IndexName)
                .Query(q => q.Terms(t => t.Field(termsQuery.Field).Terms(termsQuery.Terms))));
            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> PrefixQuery(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
                .Index(IndexName)
                .Query(q => q.Prefix(m => m
                    .Field(f => f.CustomerFullName.Suffix("keyword")).Value(customerFullName))));
            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id; // Assign the Id from the hit to the source object
            return result.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> Range(double fromPrice, double toPrice)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
                .Index(IndexName)
                .Query(q => q.Range(r => r
                    .Field(f => f.TaxFulTotalPrice)
                    .GreaterThanOrEquals(fromPrice)
                    .LessThanOrEquals(toPrice))));
            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> MatchAll()
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
                .Index(IndexName)
                .Query(q => q.MatchAll()));
            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id; 
            return result.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> PaginationMatchAll(int page,int pageSize)
        {
            var pageFrom=(page-1)*pageSize;
            var result = await _client.SearchAsync<ECommerce>(s => s
                .Index(IndexName).Size(pageSize).From(pageFrom)
                .Query(q => q.MatchAll()));
            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> WildCardQuery(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
                .Index(IndexName)
                .Query(q => q.Wildcard(q =>q.Field(f => f.CustomerFullName.Suffix("keyword"))
                    .Wildcard(customerFullName))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id; // Assign the Id from the hit to the source object
            return result.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> FuzzyQuery(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
                .Index(IndexName)
                .Query(q => q.Fuzzy(fu => fu
                    .Field(f => f.CustomerFullName.Suffix("keyword"))
                    .Value(customerFullName))));
                   
            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id; // Assign the Id from the hit to the source object
            return result.Documents.ToImmutableList();
        }
    }

}