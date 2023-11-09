using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;

namespace Repositories {
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DbConnection _context = new ();
        public Company Add(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return company;
        }

        public bool Edit(Company company)
        {
            _context.Companies.Update(company);
            _context.SaveChanges();
            return true;
        }

        public List<Company> GetAll(Func<Company, bool>? query)
        {
            var companies = _context.Companies
                .Where(
                    company => company.isActive == true
                ).Include(
                    c => c.addresses
                );
            if (query != null) {
                try {
                    return companies.Where(query).ToList();
                } catch (Exception) {
                    throw new Exception("Invalid query on CompanyRepository.GetAll");
                }
            }

            return companies.ToList();
        }

        public Company? GetById(int id)
        {
            Company? company;
            try {
                company = _context.Companies
                    .Where(
                        company => company.id == id
                    ).Where(
                        c => c.isActive == true
                    ).Include(
                        c => c.addresses
                    ).First();
            } catch (Exception) {
                company = null;
            };

            return company;
        }
    }
}