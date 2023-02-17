using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Header;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Contacts;

namespace ServicesAPI.BusinessLogic.Services.Contact
{
    public class Contact : IContact
    {
        private readonly ContextDB _contextDB;
        private Contacts _model;

        public Contact(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task<Contacts> GetContactAsync()
        {
            try
            {
                return await _contextDB.Contacts.FirstAsync();
            }
            catch
            {
                return await AddContactAsync();
            }
        }


        public async Task<Contacts> AddContactAsync()
        {
            _model = new Contacts()
            {
                CompanyAdress = "Москва, ул. Московская, дом 100",
                CompanyNumber = "8 999 999 99 99",
                CompanyEmail = "mail@mail.ru"
            };
            await _contextDB.Contacts.AddAsync(_model);
            await _contextDB.SaveChangesAsync();
            return _model;
        }


        public async Task<Contacts> UpdateContactAsync(Contacts cont)
        {
            _contextDB.Contacts.Update(cont);
            await _contextDB.SaveChangesAsync();
            return cont;
        }      
    }
}
