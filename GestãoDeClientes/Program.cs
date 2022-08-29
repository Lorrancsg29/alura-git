using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestãoDeClientes
{
    class Program
    {
        [System.Serializable]
        struct Cliente 
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        
        enum Menu { Listagem = 1, Adcionar, Remover, Sair}

        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;

            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de cliente - Bem Vindo!");
                Console.WriteLine("1-Listagem\n2-Adcionar\n3-Remover\n4-Sair");
                int intop = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intop;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adcionar:
                        Adcionar();
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

        static void Adcionar() 
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente:");
            Console.WriteLine("Nome do cliente:");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluido, aperte enter para sair");
            Console.ReadLine();
           
        }

        static void Listagem() 
        {
            if (clientes.Count >= 1)
            {
                int i = 1;
                Console.WriteLine("Listagem de clientes:");

                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("===============================");
                    i++;
                }
                Console.WriteLine("Aperte enter para sair.");
                Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Nenhum cliente cadastrado, aperte enter para retornar");
                Console.ReadLine();
            }

        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID co cliente que você quer remover:");
            int id = int.Parse(Console.ReadLine());
            if (id>=1 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID digitado é invalido, tente novamente!");
            }
        }
        
        
        
        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream,clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try
            {                
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }

                
            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();
               
            }

            stream.Close();

        }
    }
}
