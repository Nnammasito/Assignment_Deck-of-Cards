using System;
using System.Collections.Generic;

class Carta
{
    public string Nombre { get; set; }
    public string Pinta { get; set; }
    public int Val { get; set; }

    public Carta(string nombre, string pinta, int val)
    {
        Nombre = nombre;
        Pinta = pinta;
        Val = val;
    }

    public void Print()
    {
        Console.WriteLine($"{Nombre} de {Pinta} (Valor: {Val})");
    }
}

class Mazo
{
    public List<Carta> Cartas { get; set; }

    public Mazo()
    {
        Cartas = new List<Carta>();

        string[] pintas = { "Tréboles", "Picas", "Corazones", "Diamantes" };
        string[] nombres = { "As", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Reina", "Rey" };

        foreach (var pinta in pintas)
        {
            for (int i = 0; i < 13; i++)
            {
                Carta nuevaCarta = new Carta(nombres[i], pinta, i + 1);
                Cartas.Add(nuevaCarta);
            }
        }
    }

    public Carta Repartir()
    {
        if (Cartas.Count > 0)
        {
            Carta cartaRepartida = Cartas[Cartas.Count - 1];
            Cartas.RemoveAt(Cartas.Count - 1);
            return cartaRepartida;
        }
        else
        {
            Console.WriteLine("No quedan cartas para repartir.");
            return null;
        }
    }

    public void Reiniciar()
    {
        Cartas.Clear();
        Cartas = new Mazo().Cartas;
    }

    public void Barajar()
    {
        Random random = new Random();
        int n = Cartas.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Carta carta = Cartas[k];
            Cartas[k] = Cartas[n];
            Cartas[n] = carta;
        }
    }
}

class Jugador
{
    public string Nombre { get; set; }
    public List<Carta> Mano { get; set; }

    public Jugador(string nombre)
    {
        Nombre = nombre;
        Mano = new List<Carta>();
    }

    public Carta Robar(Mazo mazo)
    {
        Carta cartaRobada = mazo.Repartir();
        if (cartaRobada != null)
        {
            Mano.Add(cartaRobada);
        }
        return cartaRobada;
    }

    public Carta Descartar(int indice)
    {
        if (indice >= 0 && indice < Mano.Count)
        {
            Carta cartaDescartada = Mano[indice];
            Mano.RemoveAt(indice);
            return cartaDescartada;
        }
        else
        {
            Console.WriteLine("Índice de descarte no válido.");
            return null;
        }
    }

    public void ImprimirMano()
    {
        Console.WriteLine($"{Nombre}'s Mano:");
        foreach (var carta in Mano)
        {
            carta.Print();
        }
    }
}

class Program
{
    static void Main()
    {
        Mazo mazo = new Mazo();
        mazo.Barajar();

        Jugador jugador1 = new Jugador("Jugador 1");
        Jugador jugador2 = new Jugador("Jugador 2");

        jugador1.Robar(mazo);
        jugador1.Robar(mazo);
        jugador2.Robar(mazo);
        jugador2.Robar(mazo);

        jugador1.ImprimirMano();
        jugador2.ImprimirMano();
    }
}
