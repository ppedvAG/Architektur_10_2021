using ppedv.Laureatus.Logic;
using ppedv.Laureatus.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ppedv.Laureatus.UI.Wpf.ViewModels
{

    public class PersonViewModel : ViewModelBase
    {
        public Person SelectedPerson
        {
            get => selectedPerson;
            set
            {
                selectedPerson = value;
                OnPropertyChanged(""); //alle
                //OnPropertyChanged(nameof(Alter));
                //OnMyPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public ObservableCollection<Person> Personen { get; set; }


        public int Alter
        {
            get
            {
                return random.Next();
            }
        }


        Random random = new Random();

        Core core = new Core(new Data.EfCore.EfRepository());
        private Person selectedPerson;

        public PersonViewModel()
        {
            Personen = new ObservableCollection<Person>(core.Repository.Query<Person>().ToList());
            SaveCommand = new RelayCommand(x => core.Repository.Save());

            NewCommand = new RelayCommand(UserWantsToAddNewPerson);
            DeleteCommand = new RelayCommand(UserWantsToDeleteSelectedPerson);
        }

        private void UserWantsToDeleteSelectedPerson(object obj)
        {
            if (selectedPerson != null)
            {
                core.Repository.Delete(SelectedPerson);
                Personen.Remove(selectedPerson);
            }
        }

        private void UserWantsToAddNewPerson(object obj)
        {
            var p = new Person() { Name = "WPF NEU" };
            core.Repository.Add(p);
            Personen.Add(p);
        }
    }
}
