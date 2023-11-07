using Interfaces;
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

        public List<Company> GetAll()
        {
            return _context.Companies.ToList();
        }

        public Company? GetById(int id)
        {
            Company? company;
            try {
                company = _context.Companies.Where(
                    company => company.id == id
                ).First();
            } catch (Exception) {
                company = null;
            };

            return company;
        }
    }
}