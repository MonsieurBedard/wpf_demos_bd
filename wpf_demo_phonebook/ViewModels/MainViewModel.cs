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

        public MainViewModel()
        {
            // Commands
            SearchContactCommand = new RelayCommand(SearchContact);

            // Values init
            SelectedContact = PhoneBookBusiness.GetContactByID(1);
            Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAll().ToList());
        }

        private void SearchContact(object parameter)
        {
            string input = parameter as string;
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
                    SelectedContact = PhoneBookBusiness.GetContactByID(output);
                    break;
                case "name":
                    SelectedContact = PhoneBookBusiness.GetContactByName(input);
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }
        }
    }
}
