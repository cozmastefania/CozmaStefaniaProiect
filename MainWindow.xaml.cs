using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CabVetModel;
using System.Data.Entity;
using System.Data;

namespace CozmaStefaniaProiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Save,
        Nothing
    }
    public partial class MainWindow : Window
    {

        ActionState action = ActionState.Nothing;
        CabVetEntitateModel ctx = new CabVetEntitateModel();
        CollectionViewSource clientViewSource;
        CollectionViewSource animalViewSource;
        // CollectionViewSource cabinetViewSource;
        CollectionViewSource clientCabinetsViewSource;
        //CollectionViewSource animalCabinetsViewSource;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            clientViewSource.Source = ctx.Clients.Local;
            clientCabinetsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientCabinetsViewSource")));
            //clientViewSource.Source = ctx.Cabinets.Local;

            animalViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("animalViewSource")));
            animalViewSource.Source = ctx.Animals.Local;
            //animalCabinetsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("animalCabinetsViewSource")));

            //cabinetViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cabinetViewSource")));
            //cabinetViewSource.Source = ctx.Cabinets.Local;
            

            ctx.Clients.Load();
            ctx.Cabinets.Load();
            ctx.Animals.Load();
            cmbClient.ItemsSource = ctx.Clients.Local;
            //cmbClient.DisplayMemberPath = "Nume";
            cmbClient.SelectedValuePath = "IdClient";
            cmbAnimal.ItemsSource = ctx.Animals.Local;
            //cmbAnimal.DisplayMemberPath = "Specie";
            cmbAnimal.SelectedValuePath = "IdAnimal";

            //clientViewSource.Source = ctx.Animals.Local;

            //System.Windows.Data.CollectionViewSource clientCabinetsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientCabinetsViewSource")));
            BindDataGrid();
        }
            

            private void btnSave_Click(object sender, RoutedEventArgs e)
            {
                Client client = null;

                if (action == ActionState.New)
                {
                    try
                    {
                        client = new Client()
                        {
                            Nume = numeTextBox.Text.Trim(),
                            Prenume = prenumeTextBox.Text.Trim()
                        };
                        ctx.Clients.Add(client);
                        clientViewSource.View.Refresh();
                        ctx.SaveChanges();

                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                
                }
           

                else if (action == ActionState.Edit)
                {
                    try
                    {
                        client = (Client)clientDataGrid.SelectedItem;
                        client.Nume = numeTextBox.Text.Trim();
                        client.Prenume = prenumeTextBox.Text.Trim();
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    clientViewSource.View.Refresh();
                    clientViewSource.View.MoveCurrentTo(client);
                }
                else if (action == ActionState.Delete)
                {
                    try
                    {
                        client = (Client)clientDataGrid.SelectedItem;
                        ctx.Clients.Remove(client);
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    clientViewSource.View.Refresh();
                
            }
            SetValidationBinding();
        }

            private void btnPrev_Click(object sender, RoutedEventArgs e)
            {
                clientViewSource.View.MoveCurrentToPrevious();
            }

            private void btnNext_Click(object sender, RoutedEventArgs e)
            {
                clientViewSource.View.MoveCurrentToNext();
            }


            private void btnDelete_Click(object sender, RoutedEventArgs e)
            {
                Client client = null;
                client = (Client)clientDataGrid.SelectedItem;
                ctx.Clients.Remove(client);
            }

            private void btnEdit_Click(object sender, RoutedEventArgs e)
            {
                action = ActionState.Edit;
                BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
                BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
                SetValidationBinding();
            }

            private void btnNew_Click(object sender, RoutedEventArgs e)
            {
                Client client = null;
                client = new Client()
                {
                    Nume = numeTextBox.Text.Trim(),
                    Prenume = prenumeTextBox.Text.Trim()
                };
                ctx.Clients.Add(client);
            }

            private void btnSave1_Click(object sender, RoutedEventArgs e)
            {
                Animal animal = null;

                if (action == ActionState.New)
                {
                    try
                    {
                        animal = new Animal()
                        {
                            Afectiune = afectiuneTextBox.Text.Trim(),
                            Rasa = rasaTextBox.Text.Trim(),
                            Specie = specieTextBox.Text.Trim()
                        };
                        ctx.Animals.Add(animal);
                        animalViewSource.View.Refresh();
                        ctx.SaveChanges();

                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                else if (action == ActionState.Edit)
                {
                    try
                    {
                        animal = (Animal)animalDataGrid.SelectedItem;
                        animal.Afectiune = afectiuneTextBox.Text.Trim();
                        animal.Rasa = rasaTextBox.Text.Trim();
                        animal.Specie = specieTextBox.Text.Trim();
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    animalViewSource.View.Refresh();
                    animalViewSource.View.MoveCurrentTo(animal);
                }
                else if (action == ActionState.Delete)
                {
                    try
                    {
                        animal = (Animal)animalDataGrid.SelectedItem;
                        ctx.Animals.Remove(animal);
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                   animalViewSource.View.Refresh();
                }

            }

            private void btnPrev1_Click(object sender, RoutedEventArgs e)
            {
                animalViewSource.View.MoveCurrentToPrevious();
            }

            private void btnNext1_Click(object sender, RoutedEventArgs e)
            {
                animalViewSource.View.MoveCurrentToNext();
            }

            private void btnDelete1_Click(object sender, RoutedEventArgs e)
            {
                Animal animal = null;
                animal = (Animal)animalDataGrid.SelectedItem;
                ctx.Animals.Remove(animal);
            }

            private void btnEdit1_Click(object sender, RoutedEventArgs e)
            {
                Animal animal = null;
                animal = (Animal)animalDataGrid.SelectedItem;
                animal.Afectiune = afectiuneTextBox.Text.Trim();
                animal.Rasa = rasaTextBox.Text.Trim();
                animal.Specie = specieTextBox.Text.Trim();
            }

            private void btnNew1_Click(object sender, RoutedEventArgs e)
            {
                Animal animal = null;
                animal = new Animal()
                {
                    Afectiune = afectiuneTextBox.Text.Trim(),
                    Rasa = rasaTextBox.Text.Trim(),
                    Specie = specieTextBox.Text.Trim()
                };
                ctx.Animals.Add(animal);
            }

            private void btnSave2_Click(object sender, RoutedEventArgs e)
            {
                Cabinet cabinet = null;
                if (action == ActionState.New)
                {
                    try
                    {
                        Client client = (Client)cmbClient.SelectedItem;
                        Animal animal = (Animal)cmbAnimal.SelectedItem;

                        cabinet = new Cabinet()
                        {

                            IdClient = client.IdClient,
                            IdAnimal = animal.IdAnimal
                        };

                        ctx.Cabinets.Add(cabinet);
                        clientCabinetsViewSource.View.Refresh();

                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (action == ActionState.Edit)
                { dynamic selectedCabinet = cabinetsDataGrid.SelectedItem;
                    try
                    {
                        int curr_id = selectedCabinet.IdCabinet;
                        var editedCabinet = ctx.Cabinets.FirstOrDefault(s => s.IdCab == curr_id);
                        if (editedCabinet != null)
                        {
                            editedCabinet.IdClient = Int32.Parse(cmbClient.SelectedValue.ToString());
                            editedCabinet.IdAnimal = Convert.ToInt32(cmbAnimal.SelectedValue.ToString());
                            ctx.SaveChanges();
                        }
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    BindDataGrid();

                    clientViewSource.View.MoveCurrentTo(selectedCabinet);
                }

                else if (action == ActionState.Delete)
                {
                    try
                    {
                        dynamic selectedCabinet = cabinetsDataGrid.SelectedItem;
                        int curr_id = selectedCabinet.IdCab;
                        var deletedCabinet = ctx.Cabinets.FirstOrDefault(s => s.IdCab == curr_id);
                        if (deletedCabinet != null)
                        {
                            ctx.Cabinets.Remove(deletedCabinet);
                            ctx.SaveChanges();
                            MessageBox.Show("Cabinet Deleted Successfully", "Message");
                            BindDataGrid();
                        }
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            private void BindDataGrid()
            {
                var queryCabinet = from cab in ctx.Cabinets
                                   join cli in ctx.Clients on cab.IdClient equals
                                   cli.IdClient
                                   join ani in ctx.Animals on cab.IdAnimal equals
                                   ani.IdAnimal
                                   select new
                                   {
                                       cab.IdCab,
                                       cab.IdAnimal,
                                       cab.IdClient,
                                       cli.Nume,
                                       cli.Prenume,
                                       ani.Specie,
                                       ani.Afectiune
                                   };
                clientCabinetsViewSource.Source = queryCabinet.ToList();
            }

            private void SetValidationBinding()
                {
                    Binding numeValidationBinding = new Binding();
                    numeValidationBinding.Source = clientViewSource;
                    numeValidationBinding.Path = new PropertyPath("Nume");
                    numeValidationBinding.NotifyOnValidationError = true;
                    numeValidationBinding.Mode = BindingMode.TwoWay;
                    numeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    numeValidationBinding.ValidationRules.Add(new StringNotEmpty());
                    numeTextBox.SetBinding(TextBox.TextProperty, numeValidationBinding);

                    Binding prenumeValidationBinding = new Binding();
                    prenumeValidationBinding.Source = clientViewSource;
                    prenumeValidationBinding.Path = new PropertyPath("Prenume");
                    prenumeValidationBinding.NotifyOnValidationError = true;
                    prenumeValidationBinding.Mode = BindingMode.TwoWay;
                    prenumeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    prenumeValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
                    prenumeTextBox.SetBinding(TextBox.TextProperty, prenumeValidationBinding);
                }

            }
    }

