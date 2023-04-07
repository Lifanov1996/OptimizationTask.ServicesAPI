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
        private ILogger<Contact> _logger;

        public Contact(ContextDB contextDB, ILogger<Contact> logger)
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Инициализирован Contact");
        }

        public async Task<Contacts> GetContactAsync()
        {
            try
            {
                return await _contextDB.Contacts.FirstAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Ошибка загрузки контактной информации");
            }
        }


        public async Task<Contacts> AddContactAsync()
        {
            _model = new Contacts()
            {
                CompanyAdress = "Москва, ул. Московская, дом 100",
                CompanyNumber = "89999999999",
                CompanyEmail = "mail@mail.ru"
            };
            await _contextDB.Contacts.AddAsync(_model);
            await _contextDB.SaveChangesAsync();
            return _model;
        }


        public async Task<Contacts> UpdateContactAsync(Contacts cont)
        {
            try
            {
                var isData = await _contextDB.Contacts.AnyAsync(x => x.Id == cont.Id);
                if (!isData)
                {
                    throw new Exception("Ошибка изменения контактной информации");
                }
                _contextDB.Contacts.Update(cont);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Изменена контакная информация");
                return cont;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }      
    }
}
