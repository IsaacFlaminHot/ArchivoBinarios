using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class ArchivoBinarioEmpleados
    {
        //declaracion de flujos
        BinaryWriter bw = null;
        BinaryReader br = null;

        //Campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        public void CrearArchivo(string Archivo)
        {
            //variable locar método
            char resp;

            try
            {
                // Crear del flujo para escrbibir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
                //Captura de datos

                do
                {
                    Console.Clear();
                    Console.Write("Numero del Empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());
                    Console.Write("Nombre del Empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Direccion de Empleado: ");
                    Direccion = Console.ReadLine();
                    Console.Write("Telefono del Empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());
                    Console.Write("Dias trabajados del Empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.Write("Salario Diario del Empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());

                    //escribe los datos al archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\nDeseas Almacenar otro Registro (s/n) ?");
                    resp = Char.Parse(Console.ReadLine());
                } while ((resp == 's') || (resp == 'S'));
            }
            catch (IOException e)
            {
                Console.WriteLine("\nError : " + e.Message);
                Console.WriteLine("\nRuta : " + e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close(); //cierra el flujo - escritura
                Console.Write("\nPreseione <enter> para terminar la Escritura de Datos y regresar al Menu. ");
                Console.ReadKey();
            }
        }

        public void MostrarArchivos(string Archivo)
        {
                try
                {
                    //verifica si existe el archivo
                    if (File.Exists(Archivo))
                    {
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        //despliegue de datos en pantalla
                        Console.Clear();

                        do
                        {
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            //muestra los datos
                            Console.WriteLine("Numero del Empleado : " + NumEmp);
                            Console.WriteLine("Nombre del Empleado : " + Nombre);
                            Console.WriteLine("Direccion del Empleado : " + Direccion);
                            Console.WriteLine("Telefono del Empleado : " + Telefono);
                            Console.WriteLine("Dias Trabajados del Empleado : " + DiasTrabajados);
                            Console.WriteLine("Salario Diaria del Empleado : " + SalarioDiario);
                            Console.WriteLine("SUELDO TOTAL del Empleado : {0:C}", (DiasTrabajados * SalarioDiario));
                            Console.WriteLine("\n");
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo " + Archivo + "No Existe en el Disco!!");
                        Console.Write("\nPresione <enter> para Continuar...");
                        Console.ReadKey();
                    }
                }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado de Empleados");
                Console.Write("\nPresione <enter> para Continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close(); //cierra flujo
                Console.Write("\nPresione <enter> para terminar la lectura de Datos y regresar al Menu.");
                Console.ReadKey();
            }
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //declaracion variables auxiliares
            string Arch = null;
            int opcion;

            //cracion del objeto
            ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();

            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS ***");
                Console.WriteLine("1.- Creacion de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opcion deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                            Arch = Console.ReadLine();

                            //verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo {s/n}  ?");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                            {
                                A1.CrearArchivo(Arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del archivo que deseas Leer: ");
                            Arch = Console.ReadLine();
                            A1.MostrarArchivos(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <Enter> para Salir del Programa.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opcion No Existe!!, Presione <Enter para continuar...>");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}
