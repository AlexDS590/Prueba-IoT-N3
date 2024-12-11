using System.Collections.ObjectModel;
using AppAlumnos.Modelos.Modelos;
using Firebase.Database;

namespace AppAlumnos.AppMovil.Vistas;

public partial class ListarAlumnos : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://alumnosprueba-a7b1b-default-rtdb.firebaseio.com/");
    public ObservableCollection<Alumno> Lista { get; set; } = new ObservableCollection<Alumno>();

    public ListarAlumnos()
    {
        InitializeComponent();
        BindingContext = this;
        CargarAlumnos();
    }

    private void CargarAlumnos()
    {
        client.Child("Alumnos").AsObservable<Alumno>().Subscribe(alumno =>
        {
            if (alumno.Object != null)
            {
                Lista.Add(alumno.Object);
            }
        });
    }

    private void filtroSerachBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        string filtro = filtroSerachBar.Text.ToLower();

        if (filtro.Length > 0)
        {
            ListarCollection.ItemsSource = Lista.Where(x => x.NombreCompleto.ToLower().Contains(filtro));
        }
        else
        {
            ListarCollection.ItemsSource = Lista;
        }
    }

    private async void AgregarAlumnoButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearAlumno());
    }
}
