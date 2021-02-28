using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MiniMarket.Helper;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MiniMarket.Views;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MiniMarket.Model.Entities;
using MiniMarket.Model.EntitiesForView;

namespace MiniMarket.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private ReadOnlyCollection<CommandViewModel> _Commands;
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        #endregion

        #region Commands
        private BaseCommand _NowyTowarCommand;
        private BaseCommand _TowaryCommand;
        private BaseCommand _NowyKontrahentCommand;
        private BaseCommand _KontrahenciCommand;
        private BaseCommand _NowaFakturaZakupuCommand;
        private BaseCommand _FakturyZakupuCommand;
        private BaseCommand _PZCommand;
        private BaseCommand _WZCommand;
        private BaseCommand _FakturaSprzedazyCommand;
        private BaseCommand _FakturySprzedazyCommand;
        private BaseCommand _ParagonyCommand;
        private BaseCommand _ParagonCommand;
        private BaseCommand _ZamowieniaCommand;
        private BaseCommand _ZamowienieCommand;
        private BaseCommand _RezerwacjeCommand;
        private BaseCommand _RezerwacjaCommand;
        private BaseCommand _MagazynCommand;
        private BaseCommand _UzytkownicyCommand;
        private BaseCommand _FirmyCommand;
        private BaseCommand _GrupyTowaroweCommand;
        private BaseCommand _KategorieCommand;
        private BaseCommand _LogowanieCommand;
        private BaseCommand _WszystkieWZCommand;
        private BaseCommand _WszystkiePZCommand;
        private BaseCommand _NowaKategoriaCommand;
        private BaseCommand _NowaGrupaCommand;
        private BaseCommand _NowaFirmaCommand;
        private BaseCommand _NowyUzytkownikCommand;
        private BaseCommand _RaportBrakowCommand;
        private BaseCommand _RaportSprzedazCommand;
        private BaseCommand _RaportZyskCommand;
        
        public ICommand NowyTowarCommand
        {
            get
            {
                if (_NowyTowarCommand == null)
                {
                    _NowyTowarCommand = new BaseCommand(() => CreateView(new NowyTowarViewModel()));

                }
                return _NowyTowarCommand;
            }
        }
        public ICommand TowaryCommand
        {
            get
            {
                if (_TowaryCommand == null)
                {
                    _TowaryCommand = new BaseCommand(() => this.ShowAllTowar());

                }
                return _TowaryCommand;
            }
        }
        public ICommand NowyKontrahentCommand
        {
            get
            {
                if (_NowyKontrahentCommand == null)
                {
                    _NowyKontrahentCommand = new BaseCommand(() => CreateView(new KontrahentViewModel()));

                }
                return _NowyKontrahentCommand;
            }
        }
        public ICommand KontrahenciCommand
        {
            get
            {
                if (_KontrahenciCommand == null)
                {
                    _KontrahenciCommand = new BaseCommand(() => this.ShowAllCustomers());

                }
                return _KontrahenciCommand;
            }
        }
        public ICommand NowaFakturaZakupuCommand
        {
            get
            {
                if (_NowaFakturaZakupuCommand == null)
                {
                    _NowaFakturaZakupuCommand = new BaseCommand(() => CreateView(new NowaFakturaZakupuViewModel()));

                }
                return _NowaFakturaZakupuCommand;
            }
        }
        public ICommand FakturyZakupuCommand
        {
            get
            {
                if (_FakturyZakupuCommand == null)
                {
                    _FakturyZakupuCommand = new BaseCommand(() => this.ShowAllFakturyZakupu());

                }
                return _FakturyZakupuCommand;
            }
        }
        public ICommand RaportBrakowCommand
        {
            get
            {
                if (_RaportBrakowCommand == null)
                {
                    _RaportBrakowCommand = new BaseCommand(() => this.ShowAllRaportBrakow());

                }
                return _RaportBrakowCommand;
            }
        }
        public ICommand RaportSprzedazCommand
        {
            get
            {
                if (_RaportSprzedazCommand == null)
                {
                    _RaportSprzedazCommand = new BaseCommand(() => this.ShowAllRaportSprzedaz());

                }
                return _RaportSprzedazCommand;
            }
        }
        public ICommand PZCommand
        {
            get
            {
                if (_PZCommand == null)
                {
                    _PZCommand = new BaseCommand(() => CreateView(new PZViewModel()));

                }
                return _PZCommand;
            }
        }
        public ICommand WZCommand
        {
            get
            {
                if (_WZCommand == null)
                {
                    _WZCommand = new BaseCommand(() => CreateView(new WZViewModel()));

                }
                return _WZCommand;
            }
        }
        public ICommand FakturaSprzedazyCommand
        {
            get
            {
                if (_FakturaSprzedazyCommand == null)
                {
                    _FakturaSprzedazyCommand = new BaseCommand(() => CreateView(new FakturaSprzedazyViewModel()));

                }
                return _FakturaSprzedazyCommand;
            }
        }
        public ICommand FakturySprzedazyCommand
        {
            get
            {
                if (_FakturySprzedazyCommand == null)
                {
                    _FakturySprzedazyCommand = new BaseCommand(() => this.ShowAllFakturySprzedazy());

                }
                return _FakturySprzedazyCommand;
            }
        }
        public ICommand ParagonyCommand
        {
            get
            {
                if (_ParagonyCommand == null)
                {
                    _ParagonyCommand = new BaseCommand(() => this.ShowAllParagony());

                }
                return _ParagonyCommand;
            }
        }
        public ICommand ParagonCommand
        {
            get
            {
                if (_ParagonCommand == null)
                {
                    _ParagonCommand = new BaseCommand(() => CreateView(new ParagonViewModel()));

                }
                return _ParagonCommand;
            }
        }
        public ICommand ZamowieniaCommand
        {
            get
            {
                if (_ZamowieniaCommand == null)
                {
                    _ZamowieniaCommand = new BaseCommand(() => this.ShowAllZamowienia());

                }
                return _ZamowieniaCommand;
            }
        }
        public ICommand ZamowienieCommand
        {
            get
            {
                if (_ZamowienieCommand == null)
                {
                    _ZamowienieCommand = new BaseCommand(() => CreateView(new ZamowienieViewModel()));

                }
                return _ZamowienieCommand;
            }
        }
        public ICommand RezerwacjeCommand
        {
            get
            {
                if (_RezerwacjeCommand == null)
                {
                    _RezerwacjeCommand = new BaseCommand(() => this.ShowAllRezerwacje());

                }
                return _RezerwacjeCommand;
            }
        }
        public ICommand RezerwacjaCommand
        {
            get
            {
                if (_RezerwacjaCommand == null)
                {
                    _RezerwacjaCommand = new BaseCommand(() => CreateView(new RezerwacjaViewModel()));

                }
                return _RezerwacjaCommand;
            }
        }
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }
        public ICommand MagazynCommand
        {
            get
            {
                if (_MagazynCommand == null)
                {
                    _MagazynCommand = new BaseCommand(() => this.ShowAllStock());

                }
                return _MagazynCommand;
            }
        }
        public ICommand UzytkownicyCommand
        {
            get
            {
                
                
                if (_UzytkownicyCommand == null)
                {
                    _UzytkownicyCommand = new BaseCommand(() => this.AllUsers());

                }
                return _UzytkownicyCommand;
            }
            
        }
        public ICommand FirmyCommand
        {
            get
            {
              
                if (_FirmyCommand == null)
                {
                    _FirmyCommand = new BaseCommand(() => this.AllCompanies());

                }
                return _FirmyCommand;
            }

        }
        public ICommand GrupyTowaroweCommand
        {
            get
            {

                if (_GrupyTowaroweCommand == null)
                {
                    _GrupyTowaroweCommand = new BaseCommand(() => this.AllPrroductGroups());

                }
                return _GrupyTowaroweCommand;
            }

        }

        public ICommand KategorieCommand
        {
            get
            {

                if (_KategorieCommand == null)
                {
                    _KategorieCommand = new BaseCommand(() => this.AllCategories());

                }
                return _KategorieCommand;
            }

        }
        public ICommand LogowanieCommand
        {
            get
            {

                if (_LogowanieCommand == null)
                {
                    _LogowanieCommand = new BaseCommand(() => this.Login());

                }
                return _LogowanieCommand;
            }

        }

       

        public ICommand WszystkieWZCommand
        {
            get
            {

                if (_WszystkieWZCommand == null)
                {
                    _WszystkieWZCommand = new BaseCommand(() => this.AllWZ());

                }
                return _WszystkieWZCommand;
            }

        }
        public ICommand WszystkiePZCommand
        {
            get
            {

                if (_WszystkiePZCommand == null)
                {
                    _WszystkiePZCommand = new BaseCommand(() => this.AllPZ());

                }
                return _WszystkiePZCommand;
            }

        }
        public ICommand NowaKategoriaCommand
        {
            get
            {

                if (_NowaKategoriaCommand == null)
                {
                    _NowaKategoriaCommand = new BaseCommand(() => CreateView(new KategoriaViewModel()));

                }
                return _NowaKategoriaCommand;
            }

        }
        public ICommand NowaGrupaCommand
        {
            get
            {

                if (_NowaGrupaCommand == null)
                {
                    _NowaGrupaCommand = new BaseCommand(() => CreateView(new GrupaTowarowaViewModel()));

                }
                return _NowaGrupaCommand;
            }

        }
        public ICommand NowaFirmaCommand
        {
            get
            {

                if (_NowaFirmaCommand == null)
                {
                    _NowaFirmaCommand = new BaseCommand(() => CreateView(new FirmaViewModel()));

                }
                return _NowaFirmaCommand;
            }

        }
        public ICommand NowyUzytkownikCommand
        {
            get
            {

                if (_NowyUzytkownikCommand == null)
                {
                    _NowyUzytkownikCommand = new BaseCommand(() => CreateView(new UzytkownikViewModel()));

                }
                return _NowyUzytkownikCommand;
            }

        }
        public ICommand RaportZyskCommand
        {
            get
            {
                if (_RaportZyskCommand == null)
                {
                    _RaportZyskCommand = new BaseCommand(() => this.ShowAllRaportZysk());

                }
                return _RaportZyskCommand;
            }
        }


        private List<CommandViewModel> CreateCommands()
        {
            Messenger.Default.Register<string>(this, open);
            Messenger.Default.Register<KategorieViewModel>(this,editKategoria);
            Messenger.Default.Register<GrupyTowaroweViewModel>(this, editGrupa);
            Messenger.Default.Register<KontrahenciViewModel>(this, editKontrahent);
            Messenger.Default.Register<WszystkieTowaryViewModel>(this, editTowar);
            Messenger.Default.Register<FakturaSprzedazyForView>(this, editFakturaSprzedazy);
            Messenger.Default.Register<FakturaZakupuForView>(this, editFakturaZakupu);
            Messenger.Default.Register<ZamowieniaForView>(this, editZamowienia);
            Messenger.Default.Register<RezerwacjeForView>(this, editRezerwacje);
            Messenger.Default.Register<PZForView>(this, editPZ);
            Messenger.Default.Register<WZForView>(this, editWZ);
            Messenger.Default.Register<Firmy>(this, editFirma);
            Messenger.Default.Register<Uzytkownicy>(this, editUzytkownicy);
            Messenger.Default.Register<MagazynForView>(this, editTowarKartoteka);


            return new List<CommandViewModel>
            {

                new CommandViewModel(
                    "Zamowienie",
                    new BaseCommand(() => this.CreateView(new ZamowienieViewModel()))),
                new CommandViewModel(
                    "Towar",
                    new BaseCommand(() => this.CreateView(new NowyTowarViewModel()))),
                new CommandViewModel(
                    "Kontrahent",
                    new BaseCommand(() => this.CreateView(new KontrahentViewModel()))),
                new CommandViewModel(
                    "FV Zakup",
                    new BaseCommand(() => this.CreateView(new NowaFakturaZakupuViewModel()))),
                new CommandViewModel(
                    "Przyjęcie PZ",
                    new BaseCommand(() => this.CreateView(new PZViewModel()))),
                new CommandViewModel(
                    "FV Sprzedaż",
                    new BaseCommand(() => this.CreateView(new FakturaSprzedazyViewModel()))),
                new CommandViewModel(
                    "Paragon",
                    new BaseCommand(() => this.CreateView(new ParagonViewModel()))),
                new CommandViewModel(
                    "Wydanie WZ",
                    new BaseCommand(() => this.CreateView(new WZViewModel()))),
                 new CommandViewModel(
                    "Rezerwacja",
                    new BaseCommand(() => this.CreateView( new RezerwacjaViewModel()))),
                  new CommandViewModel(
                    "Kategoria",
                    new BaseCommand(() => this.CreateView( new KategoriaViewModel()))),
                new CommandViewModel(
                    "Grupa",
                    new BaseCommand(() => this.CreateView( new GrupaTowarowaViewModel()))),
                new CommandViewModel(
                    "Nowa firma",
                    new BaseCommand(() => this.CreateView( new FirmaViewModel()))),
                 new CommandViewModel(
                    "Użytkownik",
                    new BaseCommand(() => this.CreateView( new UzytkownikViewModel()))),
            };
        }
        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers
        private void open(string name)// w argumencie name jest zapisana treść komunikatu odebrana przez messenger
        {
            if (name == "Fv SprzedażAdd")
            {
                this.CreateView(new FakturaSprzedazyViewModel());
            }
            if (name == "Fv zakupuAdd")
            {
                this.CreateView(new NowaFakturaZakupuViewModel());
            }
            if (name == "TowaryAdd")
            {
                this.CreateView(new NowyTowarViewModel());
            }
            if (name == "KontrahenciAdd")
            {
                this.CreateView(new KontrahentViewModel());
            }
            if (name == "KategorieAdd")
            {
                this.CreateView(new KategoriaViewModel());
            }
            if (name == "Grupy towaroweAdd")
            {
                this.CreateView(new GrupaTowarowaViewModel());
            }
            if (name == "Spis firmAdd")
            {
                this.CreateView(new FirmaViewModel());
            }
            if (name == "UżytkownicyAdd")
            {
                this.CreateView(new UzytkownikViewModel());

            }
            if (name == "ParagonyAdd")
            {
                this.CreateView(new ParagonViewModel());
            }
            if (name == "ZamówieniaAdd")
            {
                this.CreateView(new ZamowienieViewModel());
            }
            if (name == "Wszystkie WZAdd")
            {
                this.CreateView(new WZViewModel());
            }
            if (name == "Wszystkie PZAdd")
            {
                this.CreateView(new PZViewModel());
            }
            if (name == "RezerwacjeAdd")
            {
                this.CreateView(new RezerwacjaViewModel());
            }

            if (name == "GrupyAll")
            {
                this.AllPrroductGroups(true);
            }
            if (name == "KategorieZakupuAll")
            {
                this.AllCategories(true);
            }
            if (name == "KategorieSprzedazyAll")
            {
                this.AllCategories(true);
            }
            if (name == "KontrahenciAll")
            {
                this.ShowAllCustomers(true);
            }
            if (name == "KategorieAll")
            {
                this.AllCategories(true);
            }
            if (name == "TowaryAll")
            {
                this.ShowAllTowar(true);
            }
           
        }
        private void editTowarKartoteka(MagazynForView id)
        {
            this.CreateView(new KartotekaViewModel(id.Twr_IdTowaru));
        }
        private void editUzytkownicy(Uzytkownicy id)
        {
            this.CreateView(new UzytkownikEditViewModel(id.U_UzytkownikId));
        }
        private void editFirma(Firmy id)
        {
            this.CreateView(new FirmaEditViewModel(id.F_FirmaId));
        }
        private void editWZ(WZForView id)
        {
            this.CreateView(new WZEditViewModel(id.Wz_IdWZ));
        }
        private void editPZ(PZForView id)
        {
            this.CreateView(new PZEditViewModel(id.Pz_IdPZ));
        }
        private void editRezerwacje(RezerwacjeForView id)
        {
            this.CreateView(new RezerwacjaEditViewModel(id.Rez_IdRezerwacji));
        }
        private void editZamowienia(ZamowieniaForView id)
        {
            this.CreateView(new ZamowienieEditViewModel(id.Zam_IdZamowienia));
        }
        private void editFakturaZakupu(FakturaZakupuForView id)
        {
            this.CreateView(new NowaFakturaZakupuEditViewModel(id.Fvz_IdZakup));
        }
        private void editFakturaSprzedazy(FakturaSprzedazyForView id)
        {
            this.CreateView(new FakturaSprzedazyEditViewModel(id.Fvs_IdZakup));
        }
        private void editTowar(WszystkieTowaryViewModel id)
        {
            this.CreateView(new NowyTowarEditViewModel(id.WybraneId));
        }
        private void editGrupa(GrupyTowaroweViewModel id)
        {
            this.CreateView(new GrupaTowarowaEditViewModel(id.WybraneId));
        }
        private void editKategoria( KategorieViewModel id)
        {             
              this.CreateView(new KategoriaEditViewModel(id.WybranaKategoriaId));            
        }
        private void editKontrahent(KontrahenciViewModel id)
        {
            this.CreateView(new KontrahentEditViewModel(id.WybraneId));
        }
        private void CreateView(WorkspaceViewModel workspace)
        {

            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
       
        private void ShowAllTowar()
        {
            WszystkieTowaryViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is WszystkieTowaryViewModel)
                as WszystkieTowaryViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieTowaryViewModel(false);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllTowar(bool x)
        {
            WszystkieTowaryViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is WszystkieTowaryViewModel)
                as WszystkieTowaryViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieTowaryViewModel(x);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        private void ShowOrder()
        {
            ZamowieniaViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is ZamowieniaViewModel)
                as ZamowieniaViewModel;
            if (workspace == null)
            {
                workspace = new ZamowieniaViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void CreateCustomer()
        {
            KontrahentViewModel workspace = new KontrahentViewModel();
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void ShowPriceList()
        {
            CennikViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is CennikViewModel)
                as CennikViewModel;
            if (workspace == null)
            {
                workspace = new CennikViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllStock()
        {
            MagazynViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is MagazynViewModel)
                as MagazynViewModel;
            if (workspace == null)
            {
                workspace = new MagazynViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllCustomers()
        {
            KontrahenciViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is KontrahenciViewModel)
                as KontrahenciViewModel;
            if (workspace == null)
            {
                workspace = new KontrahenciViewModel(false);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllCustomers(bool x)
        {
            KontrahenciViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is KontrahenciViewModel)
                as KontrahenciViewModel;
            if (workspace == null)
            {
                workspace = new KontrahenciViewModel(x);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllFakturyZakupu()
        {
           FakturyZakupuViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is FakturyZakupuViewModel)
                as FakturyZakupuViewModel;
            if (workspace == null)
            {
                workspace = new FakturyZakupuViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllFakturySprzedazy()
        {
            FakturySprzedazyViewModel workspace =
                 this.Workspaces.FirstOrDefault(vm => vm is FakturySprzedazyViewModel)
                 as FakturySprzedazyViewModel;
            if (workspace == null)
            {
                workspace = new FakturySprzedazyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllParagony()
        {
            ParagonyViewModel workspace =
                 this.Workspaces.FirstOrDefault(vm => vm is ParagonyViewModel)
                 as ParagonyViewModel;
            if (workspace == null)
            {
                workspace = new ParagonyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllZamowienia()
        {
            ZamowieniaViewModel workspace =
                 this.Workspaces.FirstOrDefault(vm => vm is ZamowieniaViewModel)
                 as ZamowieniaViewModel;
            if (workspace == null)
            {
                workspace = new ZamowieniaViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllRezerwacje()
        {
            RezerwacjeViewModel workspace =
                 this.Workspaces.FirstOrDefault(vm => vm is RezerwacjeViewModel)
                 as RezerwacjeViewModel;
            if (workspace == null)
            {
                workspace = new RezerwacjeViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void AllUsers()
        {
            UzytkownicyViewModel workspace =
                 this.Workspaces.FirstOrDefault(vm => vm is UzytkownicyViewModel)
                 as UzytkownicyViewModel;
            if (workspace == null)
            {
                workspace = new UzytkownicyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);

        }
        private void AllCompanies()
        {
            FirmyViewModel workspace =
             this.Workspaces.FirstOrDefault(vm => vm is FirmyViewModel)
             as FirmyViewModel;
            if (workspace == null)
            {
                workspace = new FirmyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void AllPrroductGroups()
        {
            GrupyTowaroweViewModel workspace =
             this.Workspaces.FirstOrDefault(vm => vm is GrupyTowaroweViewModel)
             as GrupyTowaroweViewModel;
            if (workspace == null)
            {
                workspace = new GrupyTowaroweViewModel(false);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void AllPrroductGroups(bool x)
        {
            GrupyTowaroweViewModel workspace =
             this.Workspaces.FirstOrDefault(vm => vm is GrupyTowaroweViewModel)
             as GrupyTowaroweViewModel;
            if (workspace == null)
            {
                workspace = new GrupyTowaroweViewModel(x);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void AllCategories()
        {
            KategorieViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is KategorieViewModel)
                as KategorieViewModel;
            if (workspace == null)
            {
                workspace = new KategorieViewModel(false);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void AllCategories(bool x)
        {
            KategorieViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is KategorieViewModel)
                as KategorieViewModel;
            if (workspace == null)
            {
                workspace = new KategorieViewModel(x);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace); 
        }
        private void Login()
        {
            LogowanieView logowanieView = new LogowanieView();
            logowanieView.ShowDialog();
        }
       

        private void AllPZ()
        {
            WszystkiePZViewModel workspace =
               this.Workspaces.FirstOrDefault(vm => vm is WszystkiePZViewModel)
               as WszystkiePZViewModel;
            if (workspace == null)
            {
                workspace = new WszystkiePZViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        private void AllWZ()
        {
            WszystkieWZViewModel workspace =
              this.Workspaces.FirstOrDefault(vm => vm is WszystkieWZViewModel)
              as WszystkieWZViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieWZViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllRaportBrakow()
        {
            RaportBrakowViewModel workspace =
              this.Workspaces.FirstOrDefault(vm => vm is RaportBrakowViewModel)
              as RaportBrakowViewModel;
            if (workspace == null)
            {
                workspace = new RaportBrakowViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllRaportSprzedaz()
        {
            RaportSprzedazViewModel workspace =
              this.Workspaces.FirstOrDefault(vm => vm is RaportSprzedazViewModel)
              as RaportSprzedazViewModel;
            if (workspace == null)
            {
                workspace = new RaportSprzedazViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllRaportZysk()
        {
            RaportZyskViewModel workspace =
              this.Workspaces.FirstOrDefault(vm => vm is RaportZyskViewModel)
              as RaportZyskViewModel;
            if (workspace == null)
            {
                workspace = new RaportZyskViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        #endregion
    }
}
