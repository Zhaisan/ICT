using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2
{
    interface DataAccessLayer
    {
        ContactDTO GetContactById(string id);
        string CreateContact(ContactDTO contact);
        bool DeleteContactById(string id);
        string UpdateContact(ContactDTO contact);
        List<ContactDTO> GetAllContactsInPage(int offset, string searchingName);
        List<ContactDTO> sortingContacts();
    }

    abstract class BaseContact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Addr { get; set; }
    }

    class CreateContactCommand: BaseContact {
   
    }

    class ContactDTO : BaseContact
    {
        public string Id { get; set; }
    }

    class BLL
    {
        DataAccessLayer dal = default(DataAccessLayer);
        public BLL(DataAccessLayer dal)
        {
            this.dal = dal;
        }

        public ContactDTO GetContact(string id)
        {
            return dal.GetContactById(id);
        }

        public string CreateContact(CreateContactCommand contact)
        {
            ContactDTO contact1 = new ContactDTO();
            contact1.Id = Guid.NewGuid().ToString();
            contact1.Name = contact.Name;
            contact1.Addr = contact.Addr;
            contact1.Phone = contact.Phone;
            return dal.CreateContact(contact1);
        }
        public bool DeleteContact(string id)
        {
            return dal.DeleteContactById(id);
        }

        public string UpdateContact(ContactDTO contact)
        {
            return dal.UpdateContact(contact);
        }

        public List<ContactDTO> GetContactsInPage(int offset, string searchingName)
        {
            return dal.GetAllContactsInPage(offset, searchingName);
        }

        public List<ContactDTO> sortingContacts()
        {
            return dal.sortingContacts();
        }
    }
}
