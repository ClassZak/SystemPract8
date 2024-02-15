using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;


public enum EngineState { engineAlive, engineDead }
// Абстрактный базовый класс в иерархии.
[Serializable]
public abstract class Car
{
    public string PetName { get; set; }
    public int CurrentSpeed { get; set; }
    public int MaxSpeed { get; set; }
    protected EngineState egnState = EngineState.engineAlive;
    public EngineState EngineState
    {
        get { return egnState; }
    }
    public abstract void TurboBoost();
    public virtual void Show()
    {
        Console.WriteLine(PetName);
        Console.WriteLine(MaxSpeed);
        Console.WriteLine(CurrentSpeed);
        Console.WriteLine(egnState);
    }
    public Car() { } //конструктор по умолчанию
    public Car(string name, int maxSp, int currSp)  // конструктор с параметрами
    {
        PetName = name;
        MaxSpeed = maxSp;
        CurrentSpeed = currSp;
    }
}
[Serializable]
public class SportsCar : Car
{
    public SportsCar() { }  // конструктор по умолчанию
    public SportsCar(string name, int maxSp, int currSp) // конструктор с параметрами
    : base(name, maxSp, currSp) { }
    public override void TurboBoost()  // переопределение метода TurboBoost() обязательно!
    {
        MessageBox.Show("Таранная скорость!", "Чем быстрее, тем лучше...");
    }
}
[Serializable]
public class MiniVan : Car
{
    public MiniVan() { }  // конструктор по умолчанию
    public MiniVan(string name, int maxSp, int currSp)
    : base(name, maxSp, currSp) { }  // конструктор с параметрами
    public override void TurboBoost()
    {
        egnState = EngineState.engineDead;
        MessageBox.Show("Упс!", " Ваш блок двигателя взорвался!");
    }
}


namespace Serialization
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Машины до турбоускерения:");
            Console.ForegroundColor = ConsoleColor.DarkGray;


            MiniVan car1 = new MiniVan("Hyundai Staria", 60, 1);
            car1.Show();
            Console.WriteLine();
            SportsCar car2 = new SportsCar("Tesla", 2000, 15);
            car2.Show();
            Console.WriteLine();
            Console.ResetColor();


            Console.WriteLine("Для продолжения нажмите клавишу . . .");
            Console.ReadKey(false);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Турбоускорение машин!");
            car1.TurboBoost();
            car2.TurboBoost();
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("Машины после турбоускорения:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            car1.Show();
            Console.WriteLine();
            car2.Show();
            Console.ResetColor();


            Console.WriteLine("Для продолжения нажмите клавишу . . .");
            Console.ReadKey(false);
            Console.Clear();
            {
                Console.Write("Сереализуем объект класса MiniVan в "); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("MiniVan.bin");
                Console.ResetColor();
                BinaryFormatter formatter1 = new BinaryFormatter();
                FileStream file1 = new FileStream("MiniVan.bin", FileMode.Create, FileAccess.ReadWrite);
                formatter1.Serialize(file1, car1);
                file1.Close();

                Console.Write("Сереализуем объект класса SportsCar в "); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("SportsCar.xml"); Console.ResetColor();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SportsCar));
                FileStream file2 = new FileStream("SportsCar.xml", FileMode.Create, FileAccess.ReadWrite);
                xmlSerializer.Serialize(file2, car2);
                file2.Close();
            }
            


            Console.WriteLine("Для продолжения нажмите клавишу . . .");
            Console.ReadKey(false);
            Console.Clear();
            Console.WriteLine("Десериализируем объекты");
            {
                BinaryFormatter formatter1 = new BinaryFormatter();
                FileStream file1 = new FileStream("MiniVan.bin", FileMode.Open, FileAccess.ReadWrite);
                MiniVan miniVan=(MiniVan)formatter1.Deserialize(file1);
                Console.WriteLine("Десериализованный минивэн:");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                miniVan.Show();
                Console.ResetColor();
                file1.Close();

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SportsCar));
                FileStream file2 = new FileStream("SportsCar.xml", FileMode.Open, FileAccess.ReadWrite);
                SportsCar sportsCar = (SportsCar)xmlSerializer.Deserialize(file2);
                Console.WriteLine("Десериализованный спортивный автомобиль:");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                sportsCar.Show();
                Console.ResetColor();
                file2.Close();
            }
            Console.WriteLine("Для продолжения нажмите клавишу . . .");
            Console.ReadKey(false);
        }
    }
}
