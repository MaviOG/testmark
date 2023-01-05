
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;


int številoŠtudentov = 7;
int max_ocena = 25;
Izpit[] IzpitArray = new Izpit[številoŠtudentov];
int[] statistika = new int[številoŠtudentov];
string pot_do_datoteke = "vhod8.txt";
string pot_do_izhodne_datoteke = "izhod8.txt";
PreberiIzDatoteke(pot_do_datoteke, IzpitArray);
IzracunajStatistiko(statistika, IzpitArray);
UrediPoOceni(ref IzpitArray);
ShraniVDatoteko(pot_do_izhodne_datoteke, IzpitArray, statistika);
//markmark
//vid kajk

void ShraniVDatoteko(string pot_do_izhodne_datoteke, Izpit[] IzpitArray, int[] statistika)
{
    // Create a file to write to.
    using (StreamWriter sw = File.CreateText(pot_do_izhodne_datoteke))
    {


        sw.WriteLine("Najvisja ocena :" + statistika[statistika.Length - 1]);
        sw.WriteLine("Najnizja ocena :" + statistika[0]);
        sw.WriteLine("Povpreèna ocena :" + povprecje(statistika));
        sw.WriteLine("Število pozitivnih :" + pozitivni(statistika));
        sw.WriteLine("Število negativnih :" + negativni(statistika));
        for (int i = številoŠtudentov - 1; i >= 0; i--)
        {
            sw.WriteLine(IzpisArray(i));
        }
    }
};
int negativni(int[] statistika)
{

    int negativni = 0;
    for (int i = 0; i < statistika.Length; i++)
    {
        if (statistika[i] < max_ocena / 2)
        {
            negativni++;
        }
    }
    return negativni;
};
int pozitivni(int[] statistika)
{

    int pozitivni = 0;
    for (int i = 0; i < statistika.Length; i++)
    {
        if (statistika[i] > max_ocena / 2)
        {
            pozitivni++;
        }
    }
    return pozitivni;
};
double povprecje(int[] statistika)
{
    double povprecje = 0;

    for (int i = 0; i < statistika.Length; i++)
    {
        povprecje = statistika[i] + povprecje;
    }
    povprecje = povprecje / statistika.Length;
    return povprecje;

};
void UrediPoOceni(ref Izpit[] IzpitArray)
{
    for (int i = 0; i < IzpitArray.Length - 1; i++)
    {
        int j = i + 1;

        while ((j >= 1) && (IzpitArray[j].ocena < IzpitArray[j - 1].ocena))
        {

            Izpit caka = IzpitArray[j];
            IzpitArray[j] = IzpitArray[j - 1];
            IzpitArray[j - 1] = caka;
            j--;
        }
    }
    for (int i = IzpitArray.Length - 1; i >= 0; i--)
    {

    }
};
void IzracunajStatistiko(int[] statistika, Izpit[] IzpitArray)
{
    for (int i = 0; i < številoŠtudentov; i++)
    {
        statistika[i] = IzpitArray[i].ocena;
    }
    SortArray(statistika, 0, statistika.Length - 1);

}
static void PreberiIzDatoteke(string pot_do_datoteke, Izpit[] IzpitArray)
{

    int številoDvopièji = 1;
    string podatek = "";
    int stevilkaArraya = 0;
    using (StreamReader sr = File.OpenText(pot_do_datoteke))
    {
        string s;
        while ((s = sr.ReadLine()) != null)
        {


            for (int i = 0; i < s.Length; i++)
            {

                if (s[i] == ';')
                {
                    i++;

                    if (številoDvopièji == 1)
                    {
                        IzpitArray[stevilkaArraya] = new Izpit();

                        IzpitArray[stevilkaArraya].idum = podatek;


                    }
                    if (številoDvopièji == 2)
                    {
                        IzpitArray[stevilkaArraya].ocena = Convert.ToInt32(podatek);


                    }
                    if (številoDvopièji == 3)
                    {
                        IzpitArray[stevilkaArraya].max_ocena = Convert.ToInt32(podatek);


                    }

                    številoDvopièji++;
                    podatek = "";
                }

                podatek += s[i];
                if (podatek == "kolokvij")
                {
                    IzpitArray[stevilkaArraya].tipocene = tip.kolokvij;

                    stevilkaArraya++;
                    številoDvopièji = 1;
                    podatek = "";
                }
                else if (podatek == "izpit")
                {
                    IzpitArray[stevilkaArraya].tipocene = tip.izpit;

                    stevilkaArraya++;
                    številoDvopièji = 1;
                    podatek = "";
                }


            }
        }
    }
}
static int[] SortArray(int[] statistika, int leftIndex, int rightIndex)
{
    var i = leftIndex;
    var j = rightIndex;
    var pivot = statistika[leftIndex];
    while (i <= j)
    {
        while (statistika[i] < pivot)
        {
            i++;
        }

        while (statistika[j] > pivot)
        {
            j--;
        }
        if (i <= j)
        {
            int temp = statistika[i];
            statistika[i] = statistika[j];
            statistika[j] = temp;
            i++;
            j--;
        }
    }

    if (leftIndex < j)
        SortArray(statistika, leftIndex, j);
    if (i < rightIndex)
        SortArray(statistika, i, rightIndex);
    return statistika;
}
string IzpisArray(int stevilkaArraya)
{
    string ocena = Convert.ToString(IzpitArray[stevilkaArraya].ocena);
    string max_ocena = Convert.ToString(IzpitArray[stevilkaArraya].max_ocena);

    return (
    IzpitArray[stevilkaArraya].idum + ";" + ocena + ";" + max_ocena + ";" + IzpitArray[stevilkaArraya].tipocene
    );
}
public class Izpit
{

    public string idum;
    public int ocena;
    public int max_ocena;
    public tip tipocene;

    public void izpis()
    {
        Console.Write(this.idum + ";" + this.ocena + ";" + this.max_ocena + ";" + this.tipocene);
    }

}
public enum tip { izpit, kolokvij };


