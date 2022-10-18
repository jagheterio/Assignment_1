using Assignment_1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Services
{
    // This class shows the whole menu and the user can show/create/delete/edit contacts.
    internal interface IMenuManager
    {
        public void ManinMenu();
    }
    internal class MenuManager : IMenuManager
    {
        private IFilemanager _filemanager = new FileManager();
        private List<Contact> _contacts = new();
        private string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\AddressBook.json";

        // Method to show the menu
        public void ManinMenu()
        {
            Console.Clear();
            Console.WriteLine("+++++++++ MENU +++++++++");
            Console.WriteLine(" 1. View contact List");
            Console.WriteLine(" 2. Create new contact");
            Console.WriteLine(" 9. Quit");
            Console.WriteLine();
            Console.Write(" Select option >> ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewContactList();
                    break;

                case "2":
                    CreateNewContact();
                    break;

                case "9":
                    QuitApplication();
                    break;
            }
        }


        // When option 1 is selected, show all contact in a list style
        public void ViewContactList()
        {
            try { _contacts = JsonConvert.DeserializeObject<List<Contact>>(_filemanager.ReadFile(_filePath)); }
            catch { }

            Console.Clear();
            Console.WriteLine("+++++++++++++++++ CONTACT LIST +++++++++++++++++");

            if (_contacts.Count > 0)
            {
                foreach (var contact in _contacts)
                {
                    Console.WriteLine($" ID\t: {contact.Id}");
                    Console.WriteLine($" Name\t: {contact.FullName}");
                    Console.WriteLine("------------------------------------------------");
                }

                Console.WriteLine();
                Console.WriteLine(" 3\t: view contact detail");
                Console.WriteLine(" m\t: back to MENU");
                Console.WriteLine();
                Console.Write(" Select option >> ");

                var option = Console.ReadLine();

                // Jumping to method to show a selected contact
                switch(option)
                {
                    case "3":
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine();
                        Console.Write(" Select ID: ");
                        var id = Guid.Parse(Console.ReadLine());
                        ViewAContact(id);
                        break;

                    case "m":
                        BackToMenu();
                        break;
                }
            }
        }

        // When option m is selected, back to MENU
        private void BackToMenu()
        {
            Console.WriteLine(" Press ENTER to MENU...");
        }


        // When option 2 is selected, create a new contact
        public void CreateNewContact()
        {
            Console.Clear();
            Console.WriteLine("+++++ CREATE NEW CONTACT +++++");

            var contact = new Contact();
            Console.Write(" First Name: "); contact.FirstName = Console.ReadLine();
            Console.Write(" Last Name: "); contact.LastName = Console.ReadLine();
            Console.Write(" Postal Code: "); contact.PostalCode = Console.ReadLine();
            Console.Write(" Street Name: "); contact.StreetName = Console.ReadLine();
            Console.Write(" City: "); contact.City = Console.ReadLine();
            Console.Write(" Phone: "); contact.Telephone = Console.ReadLine();
            Console.Write(" Email: "); contact.Email = Console.ReadLine();

            _contacts.Add(contact);
            _filemanager.SaveFile(_filePath, JsonConvert.SerializeObject(_contacts));
        }

        // When option 3 is selected, show a selected contact
        public void ViewAContact(Guid id)
        {
            var contact = _contacts.FirstOrDefault(x => x.Id == id);

            Console.Clear();
            Console.WriteLine("+++++++++++++++++++ SELECTED CONTACT +++++++++++++++++++");

            Console.WriteLine($" ID\t\t: {contact.Id}");
            Console.WriteLine($" First name\t: {contact.FirstName}");
            Console.WriteLine($" Last name\t: {contact.LastName}");
            Console.WriteLine($" Postal Code\t: {contact.PostalCode}");
            Console.WriteLine($" Street Name\t: {contact.StreetName}");
            Console.WriteLine($" City\t\t: {contact.City}");
            Console.WriteLine($" Phone\t\t: {contact.Telephone}");
            Console.WriteLine($" Email\t\t: {contact.Email}");
            Console.WriteLine("--------------------------------------------------------");

            // 
            Console.WriteLine();
            Console.WriteLine(" e\t: Edit contact: ");
            Console.WriteLine(" d\t: Delete contact: ");
            Console.WriteLine(" m\t: Press ENTER to MENU");
            Console.WriteLine();
            Console.Write(" Select option >> ");
            var option = Console.ReadLine();


            switch (option?.ToLower())
            {
                case "e":
                    EditContact(contact);
                    break;

                case "d":
                    
                    DeleteContact(contact.Id);
                    break;

                case "m":
                    BackToMenu();
                    break;
            }
        }

        // When option e is selected, edit the contact
        private void EditContact(Contact contact)
        {
            Console.WriteLine("+++++++++++++++++++++ EDIT CONTACT +++++++++++++++++++++");

            var index = _contacts.IndexOf(contact);

            Console.Write(" First Name\t: "); contact.FirstName = Console.ReadLine();
            Console.Write(" Last Name\t: "); contact.LastName = Console.ReadLine();
            Console.Write(" Postal Code\t: "); contact.PostalCode = Console.ReadLine();
            Console.Write(" Street Name\t: "); contact.StreetName = Console.ReadLine();
            Console.Write(" City\t\t: "); contact.City = Console.ReadLine();
            Console.Write(" Phone\t\t: "); contact.Telephone = Console.ReadLine();
            Console.Write(" Email\t\t: "); contact.Email = Console.ReadLine();

            _contacts[index] = contact;
            _filemanager.SaveFile(_filePath, JsonConvert.SerializeObject(_contacts));
        }

        // When option r is selected, delete the contact
        private void DeleteContact(Guid id)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" Delete contact?");
            Console.WriteLine(" d: delete\n c: cancell");
            Console.WriteLine();
            Console.Write(" Select option >> ");
            var option = Console.ReadLine();
 

            switch (option?.ToLower())
            {
                case "d":
                    _contacts = _contacts.Where(x => x.Id != id).ToList();
                    _filemanager.SaveFile(_filePath, JsonConvert.SerializeObject(_contacts));
                    break;

                case "c":
                    BackToMenu();
                    break;
            }
           
        }

        // When option 9 is selected, quit the application
        private void QuitApplication()
        {
            Environment.Exit(0);
        }

    }
}
