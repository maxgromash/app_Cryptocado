using App5.Models;
using App5.Views;
using System;
using System.Collections.Generic;       
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App5
{
    public partial class App : Xamarin.Forms.Application
    {
        public static List<Definition> words { get; set; }
        public static bool InternetProblem { get; set; }
        public static bool UpdateDiffer { get; set; }
        //Для сериализация
        public static User user { get; set; }
        ///Адреса для сохранения данных
        public static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "data.txt");
        public static string pathCoin = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "coin.txt");
        static DataContractJsonSerializer format = new DataContractJsonSerializer(typeof(Models.User));
        static DataContractJsonSerializer formatCoin = new DataContractJsonSerializer(typeof(List<Coin>));

        /// <summary>
        /// Запуск приложжения
        /// </summary>
        public App()
        {
            Device.SetFlags(new[] { "CarouselView_Experimental", "IndicatorView_Experimental", "SwipeView_Experimental" });
            UpdateDiffer = true;

            //Если интернета нет, загружает курсы монет из памяти
            if (File.Exists(pathCoin) && Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                LoadCoins();
            }

            try
            {
                //Обновление курса - отдельный поток, так это затратно
                new Thread(() => Data.DataCourses.UpdatePrices()).Start();
            } catch (Exception)
            {
                if (File.Exists(pathCoin)) 
                    LoadCoins();
            }

            //Загрузда данных о пользователе - отдельный поток
            new Thread(() => Start()).Start();

            MainPage = new LoadPage();
          
        }


        void Start()
        {
            if (!Load()) 
                CreateNewUser();

            //Загрузка терминов из JSON файла
            DataContractSerializer format = new DataContractSerializer(typeof(List<Definition>));
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith("words.txt", StringComparison.OrdinalIgnoreCase));
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            words = (List<Definition>)format.ReadObject(stream);
            
        }
        
        bool Load()
        {
            if (File.Exists(path))
            {
                using (FileStream bas = new FileStream(path, FileMode.Open))
                {
                    bas.Position = 0;
                    while (true)
                        try
                        {
                            user = (User)format.ReadObject(bas);
                        }
                        catch (Exception)
                        {
                            bas.Close();
                            break;
                        }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод сохраняет данные о пользователе
        /// </summary>
        public static void SaveData()
        {
            using (FileStream bas = new FileStream(path, FileMode.Create))
            {
                bas.Position = 0;
                format.WriteObject(bas, user);
            }
        }
        
        /// <summary>
        /// Метод сохраняет курсы монет
        /// </summary>
        public static void SaveCoins()
        {
            using (FileStream bas = new FileStream(pathCoin, FileMode.Create))
            {
                formatCoin.WriteObject(bas, Data.DataCourses.coins);
            }
        }

        /// <summary>
        /// Метод загружает данные о монетах из памяти устройства
        /// </summary>
        public static void LoadCoins()
        {
            InternetProblem = true;
            using (FileStream bas = new FileStream(pathCoin, FileMode.Open))
            {
                Data.DataCourses.coins = (List<Coin>) formatCoin.ReadObject(bas);
            }
        }

        /// <summary>
        /// Метод создаёт объект пользователя и сохраняет его
        /// </summary>
        void CreateNewUser()
        {
            using (FileStream bas = new FileStream(path, FileMode.Create))
            {
                user = new User(30000);
                format.WriteObject(bas, user);
            }
        }

        /// <summary>
        /// Обновляет цены, при отсутствии интернета
        /// </summary>
        protected override void OnResume()
        {
            if (File.Exists(pathCoin) && Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                LoadCoins();
            }
            else
            {
                try
                {
                    new Thread(() => Data.DataCourses.UpdatePrices()).Start();
                    UpdateDiffer = true;
                } catch(Exception)
                {
                    LoadCoins();
                    InternetProblem = true;
                }
            }
            MainPage = new LoadPage();
        }
    }
}
