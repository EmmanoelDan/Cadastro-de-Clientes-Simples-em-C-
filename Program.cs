using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Projeto2
{
    class Program { 
      [System.Serializable]
      struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List <Cliente> clientes = new List<Cliente>();
        enum Menu
        {
            Listar =1, Adicionar=2, Remover=3, Sair=4
        }
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair) {
                Console.Title = "Sistema de Clientes";
                Console.WriteLine("Sistema de Clientes");
                Console.WriteLine("1.Listar\n2.Adicionar\n3.Remover\n4.Sair");
                Menu op = (Menu)int.Parse(Console.ReadLine());

                switch (op)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listar:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }      
        }
        static void Adicionar()
        {
            Console.Clear();
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Clientes");
            Console.WriteLine("Digite seu nome:");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Digite seu email:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("Digite seu CPF:");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Guardar();


            Console.WriteLine("Aperte enter p Sair.");
            Console.ReadLine();
        }
        static void Listagem()
        {
            if(clientes.Count > 0)
            {
                Console.WriteLine("LISTA DE CLIENTES");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: { cliente.nome}");
                    Console.WriteLine($"E-mail: { cliente.email}");
                    Console.WriteLine($"CPF: { cliente.cpf}");
                    Console.WriteLine("========================================================");
                    i++;

                }
                
            } else
            {
                Console.WriteLine("Nenhumm cliente cadastrado");             
            }
            Console.WriteLine("Aperte enter p Sair.");
            Console.ReadLine();
        }
        static void Remover()
        {
            Console.Clear();
            Listagem();
            Console.WriteLine("Dirgite o ID do Cliente");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Guardar();
            }else
            {
                Console.WriteLine("Id digitado invaalido, tente novamente");
                Console.ReadLine();
            }
        }
       static void Guardar()
        {
            FileStream stream = new FileStream("BancodeClientes.dat", FileMode.OpenOrCreate );
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);
            stream.Close();
           
       }
       static void Carregar()
        {
            FileStream stream = new FileStream("BancodeClientes.dat", FileMode.OpenOrCreate);
            try {
                
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);
                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }
                
            }
            catch(Exception e) {
                clientes = new List<Cliente>();
            }
            stream.Close();

        }

    }
}
