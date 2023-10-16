using System;
namespace Lab1_Bai1
{

    abstract class Shape
    {

        public string Ten { get; set; }
        public Tuple<int, int> ViTri { get; set; }
        public abstract double DienTich();
        public abstract void Ve();
    }
    class HChuNhat : Shape
    {
        public double chieuDai { get; set; }
        public double chieuRong { get; set; }

        public override double DienTich() { return chieuDai * chieuRong; }
        public override void Ve() { Console.WriteLine("Hinh chu nhat co ten '{0}' duoc ve tai vi tri {1},{2} voi chieu dai la {3}, chieu rong la {4}", Ten, ViTri.Item1, ViTri.Item2, chieuDai, chieuRong); }
    }
    class HVuong : HChuNhat
    {
        public HVuong(string ten, Tuple<int, int> vitri, double canh)
        {
            Ten = ten;
            ViTri = vitri;
            chieuDai = canh;
            chieuRong = canh;
        }
        public override void Ve()
        {
            Console.WriteLine("Hinh vuong co ten '{0}' duoc ve tai vi tri {1},{2} voi canh la {3}", Ten, ViTri.Item1, ViTri.Item2, chieuDai);
        }
    }
    class HTron : Shape
    {
        public double BanKinh { get; set; }
        public override double DienTich()
        {
            return Math.PI * BanKinh * BanKinh;
        }
        public override void Ve()
        {
            Console.WriteLine("Hinh tron co ten '{0}' duoc ve tai vi tri {1},{2} voi ban kinh la {3}", Ten, ViTri.Item1, ViTri.Item2, BanKinh);
        }
    }
    class HTamGiac : Shape
    {
        public double Day { get; set; }
        public double chieuCao { get; set; }
        public override double DienTich()
        {
            return 0.5 * chieuCao * Day;
        }
        public override void Ve()
        {
            Console.WriteLine("Hinh tam giac co ten '{0}' duoc ve tai vi tri {1},{2} voi day la {3}, chieu cao la {4}", Ten, ViTri.Item1, ViTri.Item2, Day, chieuCao);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nhap so hinh muon tao: ");
            int n = int.Parse(Console.ReadLine());
            List<Shape> shapes = new List<Shape>();
            List<Type> types = new List<Type>() { typeof(HChuNhat), typeof(HVuong), typeof(HTron), typeof(HTamGiac) };
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                Type t = types[random.Next(types.Count)];
                Console.WriteLine("Nhap ten hinh: ");
                string ten = Console.ReadLine();
                
                Console.WriteLine("Nhap vi tri x, y: ");
                string[] stringViTri = Console.ReadLine().Split(',');
                Tuple<int,int> vitri = new Tuple<int, int>(int.Parse(stringViTri[0]), int.Parse(stringViTri[1]));
                
                if (t == typeof(HChuNhat))
                {
                    Console.WriteLine("Nhap chieu dai: ");
                    double chieudai = double.Parse(Console.ReadLine());
                    Console.WriteLine("Nhap chieu rong: ");
                    double chieurong = double.Parse(Console.ReadLine());
                    HChuNhat HCN = new HChuNhat() { chieuDai = chieudai, chieuRong = chieurong, Ten = ten, ViTri = vitri };
                    shapes.Add(HCN);
                }
                else if (t == typeof(HVuong))
                {
                    Console.WriteLine("Nhap canh: ");
                    double canh = double.Parse(Console.ReadLine());
                    HVuong HV = new HVuong(ten, vitri, canh);
                    shapes.Add(HV);
                }
                else if (t == typeof(HTron))
                {
                    Console.WriteLine("Nhap ban kinh: ");
                    double bankinh = double.Parse(Console.ReadLine());
                    HTron HTron = new HTron() { Ten = ten, ViTri = vitri, BanKinh = bankinh };
                    shapes.Add(HTron);

                }
                else if (t == typeof(HTamGiac))
                {
                    Console.WriteLine("Nhap do dai day: ");
                    double day = double.Parse(Console.ReadLine());
                    Console.WriteLine("Nhap do dai chieu cao: ");
                    double chieucao = double.Parse(Console.ReadLine());
                    HTamGiac HTG = new HTamGiac() { Ten = ten, ViTri = vitri, Day = day, chieuCao = chieucao };
                    shapes.Add(HTG);
                }
            }
            foreach(Shape shape in shapes)
            {
                shape.Ve();
                Console.WriteLine("Dien tich cua hinh la {0}",shape.DienTich());
            }
        }
    }
}