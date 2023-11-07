using Interfaces;
using Models;

namespace Repositories {
    public class AddressRepository : IAddressRepository {
        private readonly DbConnection _context = new();
        public Address Add(Address address) {
            _context.Addresses.Add(address);
            _context.SaveChanges();

            return address;
        }

        public bool Edit(Address address)
        {
            _context.Addresses.Update(address);
            _context.SaveChanges();
            return true;
        }

        public List<Address> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public Address? GetById(int id)
        {
            Address? Address;
            try {
                Address = _context.Addresses.Where(
                    Address => Address.id == id
                ).First();
            } catch (Exception) {
                Address = null;
            };

            return Address;
        }
    }
}