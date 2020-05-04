using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ContactModel selectedContact;
        public ContactModel SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ContactModel> contacts;
        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        private string criteria;
        public string Criteria
        {
            get { return criteria; }
            set
            {
                criteria = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchContactCommand { get; set; }
        public RelayCommand SaveContactCommand { get; set; }
        public RelayCommand DeleteContactCommand { get; set; }
        public RelayCommand NewContactCommand { get; set; }

        public MainViewModel()
        {
            // Commands
            SearchContactCommand = new RelayCommand(SearchContact);
            SaveContactCommand = new RelayCommand(SaveContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);
            NewContactCommand = new RelayCommand(NewContact);

            RestoreContactList();
        }

        private void RestoreContactList()
        {
            Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAll().ToList());
            SelectedContact = Contacts[0];
        }

        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            if (input.Length == 0)
            {
                RestoreContactList();
            }
            else
            {
                int output;
                string searchMethod;
                if (!Int32.TryParse(input, out output))
                {
                    searchMethod = "name";
                }
                else
                {
                    searchMethod = "id";
                }

                switch (searchMethod)
                {
                    case "id":
                        Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetContactListByID(output).ToList());
                        break;
                    case "name":
                        Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetContactListByName(input).ToList());
                        break;
                    default:
                        MessageBox.Show("Unkonwn search method");
                        break;
                }

                if (Contacts.Count != 0)
                {
                    SelectedContact = Contacts[0];
                }
                else
                {
                    MessageBox.Show("No contact found");
                }
            }
        }

        private void SaveContact(object parameter)
        {
            var contact = parameter as ContactModel;
            if (!contact.isNew)
            {
                int currentIndex = Contacts.IndexOf(contact);
                PhoneBookBusiness.UpdateContactRow(contact);
                RestoreContactList();
                SelectedContact = Contacts[currentIndex];
            }
            else
            {
                PhoneBookBusiness.NewContactRow(contact);
                RestoreContactList();
                SelectedContact = Contacts[Contacts.Count - 1];

                // remove duplicate because ???
                PhoneBookBusiness.DeleteContactRow(SelectedContact);

                RestoreContactList();
                SelectedContact = Contacts[Contacts.Count - 1];
            }
        }

        private void DeleteContact(object parameter)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var contact = parameter as ContactModel;
                PhoneBookBusiness.DeleteContactRow(contact);
                RestoreContactList();
            }
        }

        private void NewContact(object parameter)
        {
            var contact = new ContactModel();
            contact.isNew = true;
            Contacts.Add(contact);
            SelectedContact = contact;
        }
    }
}
