using System;
using System.Collections.Generic;

namespace ProbandoCodigo
{
    class JuegoDeRol2
    {
        static public void Main(string[] args)
        {
            Jugador protagonista = Jugador.Introduccion();
            Console.WriteLine("Hola " + protagonista.name);
            Console.WriteLine("Despiertas y te encuentras en una sala blanca junto a otras 3 personas, no recuerdas nada");
            Console.WriteLine("Una mujer pelirroja con una bata blanca, un chico de tu edad y un anciano trans");
            Console.WriteLine("QUE HACES?\n");
            
  
            PrimeraQuest(protagonista);

            Console.ReadLine();
        }


        //===========================================FUNCIONES DE CONTROL DE FLUJO=======================================================


        static public bool ComprobarQuest(NPC npc)
        {
            if (!npc.flag1 && npc.flag2 && npc.questCompletada)
            {
                return true;
            }
            else return false;
        }


        //static public bool YesNO()
        //{
        //    Console.WriteLine("YES/NO");
        //    String respuesta = Console.ReadLine().ToUpper();
        //    while (true)
        //    {
        //        if (respuesta == "YES") { return true; }
        //        else if (respuesta == "NO") { return false; }
        //        else { Console.WriteLine("Respuesta no valida"); }
        //    }
        //}


        //===============================================MISIONES PRINCIPALES=======================================================



        static public void PrimeraQuest(Jugador protagonista)
        {
            NPC mar = new NPC("Mar");
            NPC joselito = new NPC("Joselito");
            NPC laViejaDelVisillo = new NPC("ViejaDelVisillo");
            bool terminar = false;
            while (!terminar)
            {
                Console.WriteLine("1 - Para hablar con la mujer");
                Console.WriteLine("2 - Para hablar con el chico");
                Console.WriteLine("3 - Para hablar con el anciano");
                Console.WriteLine("4 - Revisarte los bolsillos");
                Console.WriteLine("5 - Abrir la puerta de la habitacion");
                //TODO AÑADIR UN TRY-CATCH A ESTA RESPUESTA PORQUE TIRA EXCEPCION NECESITO AYUDA DE WASS
                //try
                //{
                String nombre = Console.ReadLine();
                int respuesta;
                if (int.TryParse(nombre, out respuesta));

                switch (respuesta)
                {
                    case 1:

                        if (protagonista.BuscarItem("CIGARRO"))
                        {
                            mar.questCompletada = true;
                        }
                        mar.Hablar();
                        mar.QuestMar(protagonista); 
                        //TODO Conseguir que tras completar la quest no pase por presentarse antes de que se le vuelva a solicitar
                        break;

                    case 2:
                        joselito.QuestChico(protagonista);
                        break;
                        
                    case 3:
                        laViejaDelVisillo.Hablar();
                        laViejaDelVisillo.QuestViejaDelVisillo(protagonista);

                        break;

                    case 4:
                        protagonista.AbrirInventario();
                        break;

                    case 5:
                        if (protagonista.AbrirPuerta())
                        {
                            terminar = true;
                        }
                        break;

                    default:
                        Console.WriteLine("Respuesta no valida, por favor, elige una de las siguientes: \n");
                        break;
                 }
            };
        }





        //=============================================== CLASE NPC =======================================================


        public class NPC
        {
            private String name;
            public bool flag1 = false;
            public bool flag2 = false;
            public bool questCompletada = false;
            public NPC(String name)
            {
                this.name = name;
            }


 //=============================================== MISIONES NPC =======================================================


            public void QuestMar(Jugador protagonista)
            {

                String item = "CIGARRO";
                if (protagonista.BuscarItem(item) && flag1 && !flag2)
                {
                    Console.WriteLine("Usar " + item + "?");
                    if (protagonista.YesNo() == Jugador.Respuesta.yes)
                    {
                        protagonista.RestarItem(item);
                        Console.WriteLine("AL FIN JODER, UN PUTO CIGARRO");
                        Console.WriteLine("TOMA, LARGATE DE AQUI QUE NO TE QUIERO DEBER NADA");
                        protagonista.AñadirObjetoInventario("LLAVE");
                        CompletarQuest();
                    }
                    else { Console.WriteLine("Pierdes tu mas que yo"); }

                }
                else if (!protagonista.BuscarItem(item) && flag1 && !flag2)
                {
                    Console.WriteLine("Tengo muchisimo mono, necesito un cigarrillo");
                    Console.WriteLine("Creo que ese niñato tiene");
                }
            }

            public void QuestViejaDelVisillo(Jugador protagonista)
            {
                if (flag1 && !flag2)
                {
                    Console.WriteLine("La verdad es que ya no pasa nada interesante en el pueblo" +
                    "\nasi que me he venido aqui a cotillear, jugamos mientras me cuentas algo interesante?");
                    Console.WriteLine("Juegas con el anciano, \ndurante el juego cotilleais y como agradecimiento te mete algo en el bolsillo");
                    protagonista.AñadirObjetoInventario("GRAMO DE COCA");
                    CompletarQuest();
                }
            }

            public void QuestChico(Jugador protagonista)
            {
               String item = "GRAMO DE COCA";
               if (protagonista.BuscarItem(item))
                {
                    Console.WriteLine("El que algo quiere, algo le cuesta");
                    Console.WriteLine("Usar " + item + "?");

                    switch (protagonista.YesNo())
                    {
                        case Jugador.Respuesta.yes:
                            protagonista.RestarItem("GRAMO DE COCA");
                            protagonista.AñadirObjetoInventario("CIGARRO");

                            CompletarQuest();
                            break;
                        case Jugador.Respuesta.no:
                            Console.WriteLine("Tu veras lo que haces");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Que coño miras? andá pallá bobo");
                }
            }


 //=============================================== FUNCIONES NPC =======================================================


            public void Hablar()
            {

                if (!flag1 && !flag2) // 0-0
                {
                    Console.WriteLine("Hola! Me llamo " + name + ", puedes ayudarme porfavor?");
                    while (true)
                    {
                        Console.WriteLine("YES/NO");
                        String respuesta = Console.ReadLine().ToUpper();
                        if (respuesta == "YES")
                        {
                            Console.WriteLine("Muchas gracias! Esto es lo que necesito: ");
                            flag1 = true ;   // 1-0
                            break;
                        }
                        else if (respuesta == "NO")
                        {
                            Console.WriteLine("Vale, pues que te den, hijo de puta");
                            Console.WriteLine("Largo de aqui");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Respuesta no valida");
                        }
                    }  

                }
                else if (flag1 && !flag2 && !questCompletada) // 1-0 & false
                {
                    Console.WriteLine("Por qué has vuelto sin terminar??");
                }
                else if (flag1 && flag2) // 1-1 
                {
                    Console.WriteLine("Hola de nuevo!");
                    Console.WriteLine("Muchas gracias por tu ayuda!!");
                };

            }

            public void CompletarQuest()
            {
                flag1 = true;
                flag2 = true;
                questCompletada = true; // 1-1 true
            }
        }


       //============================================== CLASE JUGADOR ========================================================

        public class Jugador
        {
            public String name;
            public String ultimate;
            public int age;
            public String arma;
            public List<String> inventario = new List<string>();
            public Jugador() { }
            public Jugador(String name, int age, String ultimate)
            {
                this.name = name;
                this.age = age;
                this.ultimate = ultimate;
            }


            //=============================================== FUNCIONES JUGADOR =======================================================

            public enum Respuesta
            {
                no,
                yes,
                notValid,
            }

            public Respuesta YesNo()
            {
                Console.WriteLine("YES/NO");
                String respuesta = Console.ReadLine().ToUpper();
                while (true)
                {
                    if (respuesta == "YES") { return Respuesta.yes; }
                    else if (respuesta == "NO") { return Respuesta.no; }
                    else { Console.WriteLine("Respuesta no valida"); return Respuesta.notValid; }
                }
                //TODO SI Introduces otra cosa que no sea un
            }

            public void AñadirObjetoInventario(String objeto)
            {
                inventario.Add(objeto);
                Console.WriteLine(objeto + " +1");
            }

            public void AbrirInventario()
            {
                if (inventario.Count == 0)
                {
                    Console.WriteLine("Tus bolsillos estan vacios");
                }
                else
                {
                    foreach (String objeto in inventario)
                    {
                        Console.WriteLine(objeto + " x1");
                    }
                }
            }

            public bool AbrirPuerta()
            {
                if (BuscarItem("LLAVE"))
                {
                    if (YesNo() == Respuesta.yes)
                    {
                        RestarItem("LLAVE");
                        Console.WriteLine("Puerta abierta");
                        return true;
                    }
                    else { };
                }
                else
                {
                    Console.WriteLine("La puerta esta cerrada");
                }

                return false;
            }

            //public bool UsarItemYesNo(String item)
            //{
            //    Console.WriteLine("Usar " + item + "?");
            //    Console.WriteLine("YES/NO");

            //    //while (true)
            //    //{
            //    //    String respuesta = Console.ReadLine().ToUpper();
            //    //    if (respuesta == "YES")
            //    //    {
            //    //        RestarItem(item);
            //    //        return true;
            //    //    }
            //    //    else if (respuesta == "NO") { return false; }
            //    //    else { Console.WriteLine("Respuesta no valida"); }
            //    //}
            //        String respuesta = Console.ReadLine().ToUpper();
            //        if (respuesta == "YES")
            //        {
            //            RestarItem(item);
            //            return true;
            //        }
            //        else if (respuesta == "NO") { return false; }
            //        else { Console.WriteLine("Respuesta no valida"); }
                
            //}

            public bool BuscarItem(String item)
            {
                if (inventario.Exists(elemento => elemento == item))
                { 
                    return true;
                }
                else
                {
                    return false;
                }
            }

                public void RestarItem(String item)
                {
                    Console.WriteLine(item + " utilizado");
                    inventario.Remove(item);
                }


        //=============================================== INTRODUCCION =======================================================


            static public Jugador Introduccion()
            {

                List<Jugador> jugadores = new List<Jugador>();
                Jugador protagonista = new Jugador();

                Console.WriteLine("Bienvenido, Elige a tu personaje: ");

                Jugador samy = new Jugador("Samy", 27, "Pegarte");
                Jugador wassim = new Jugador("Wass", 27, "Programar");
                Jugador charly = new Jugador("Charly", 28, "Insultarte");

                jugadores.Add(samy);
                jugadores.Add(charly);
                jugadores.Add(wassim);

                bool a = true;
                while (a == true)
                {
                    foreach (Jugador personaje in jugadores)
                    {
                        Console.WriteLine("Personaje: " + personaje.name);
                        Console.WriteLine("Edad: " + personaje.age);
                        Console.WriteLine("Ultimate: " + personaje.ultimate + "\n");
                    }
                    bool b = true;
                    while (b == true)
                    {
                        String eleccion = Console.ReadLine().ToUpper();
                        switch (eleccion)
                        {
                            case "SAMY":
                                Console.WriteLine("Has elegido a: " + eleccion);
                                protagonista = jugadores[0];
                                b = false;
                                break;
                            case "CHARLY":
                                Console.WriteLine("Has elegido a: " + eleccion);
                                protagonista = jugadores[1];
                                b = false;
                                break;
                            case "WASS":
                                Console.WriteLine("Has elegido a: " + eleccion);
                                protagonista = jugadores[2];
                                b = false;
                                break;
                            default:
                                Console.WriteLine("Eleccion no  valida, por favor, elige ota vez:");
                                break;

                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Confirmar eleccion? YES/NO");
                        String confirmar = Console.ReadLine().ToUpper();
                        if (confirmar == "YES") { Console.WriteLine("\nPersonaje seleccionado"); a = false; break; }
                        else if (confirmar == "NO") { Console.WriteLine("\nPorfavor, Elige a tu personaje: "); break; }
                        else { Console.WriteLine("\nEleccion no valida"); }
                    }
                }
                Console.WriteLine("\nQue comience la aventura!");
                Console.WriteLine(protagonista.name);
                return protagonista;

            }
        }

    }
}