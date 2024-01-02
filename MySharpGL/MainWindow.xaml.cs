using SharpGL;
using SharpGL.WPF;
using System.Windows;

namespace MySharpGL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Scene scene = new Scene();
        private readonly MyTeaPot teaPot = new MyTeaPot();

        public MainWindow()
        {
            InitializeComponent();
        }
        // VBO
        private void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            //scene.Draw(gl);
            teaPot.Draw(gl);
        }


        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            //scene.Initialise(gl, (float)Width, (float)Height);
            teaPot.Initialise(gl);
        }


    }
}
