using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityVisitorUniversal.AppData
{
    public class SvgHelper
    {
        /// <summary>
        /// Ид нашего региона
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Имя нашего региона
        /// </summary>
        public string Name { get; set; }
        public SKPath SKPath { get; set; }

        public SKPath TSKPath { get; set; }


        public SvgHelper() { }

        public SvgHelper(string id, SKPath path)
        {
            Id = id.Replace("\"", "").Replace("id=", "");
            //Name = name;


            switch (Id)
            {
                case "path16": Name = "Республика Адыгея"; break;
                case "path38": Name = "Республика Крым"; break;
                case "path188": Name = "город Севастополь"; break;
                case "path72": Name = "Краснодарский край"; break;
                case "path148": Name = "Ростовская область"; break;
                case "path102": Name = "Воронежская область"; break;
                case "path92": Name = "Белгородская область"; break;
                case "path30": Name = "Республика Калмыкия"; break;
                case "path90": Name = "Астраханская область"; break;
                case "path98": Name = "Волгоградская область"; break;
                case "path58": Name = "Чеченская республика"; break;
                case "path24": Name = "Республика Дагестан"; break;
                case "path80": Name = "Ставропольский край"; break;
                case "path46": Name = "Республика Северная Осетия-Алания"; break;
                case "path26": Name = "Республика Ингушетия"; break;
                case "path32": Name = "Карачаево-Черкесская республика"; break;
                case "path154": Name = "Саратовская область"; break;
                case "path140": Name = "Оренбургская область"; break;
                case "path152": Name = "Самарская область"; break;
                case "path172": Name = "Ульяновская область"; break;
                case "path144": Name = "Пензенская область"; break;
                case "path42": Name = "Республика Мордовия"; break;
                case "path162": Name = "Тамбовская область"; break;
                case "path150": Name = "Рязанская область"; break;
                case "path124": Name = "Липетская область"; break;
                case "path120": Name = "Курская область"; break;
                case "path142": Name = "Орловская область"; break;
                case "path168": Name = "Тульская область"; break;
                case "path52": Name = "Донецкая народная республика"; break;
                case "path176": Name = "Запорожская область"; break;
                case "path178": Name = "Херсонская область"; break;
                case "path60": Name = "Луганская народная республика"; break;
                case "path94": Name = "Брянская область"; break;
                case "path110": Name = "Калужская область"; break;
                case "path128": Name = "Московская область"; break;
                case "path184": Name = "Москва"; break;
                case "path160": Name = "Смоленская область"; break;
                case "path164": Name = "Тверская область"; break;
                case "path146": Name = "Псковская область"; break;
                case "path108": Name = "Калининградская область"; break;
                case "path122": Name = "Ленинградская область"; break;
                case "path134": Name = "Новгородская область"; break;
                case "path34": Name = "Республика Карелия"; break;
                case "path130": Name = "Мурманская область"; break;
                case "path88": Name = "Архангельская область"; break;
                case "path100": Name = "Вологодская область"; break;
                case "path180": Name = "Ярославская область"; break;
                case "path116": Name = "Костромская область"; break;
                case "path96": Name = "Владимировская область"; break;
                case "path104": Name = "Ивановская область"; break;
                case "path132": Name = "Нижегородская область"; break;
                case "path62": Name = "Чувашская Республика"; break;
                case "path40": Name = "Республика Марий Эл"; break;
                case "path48": Name = "Республика Татарстан"; break;
                case "path20": Name = "Республика Башкортостан"; break;
                case "path174": Name = "Челябинская область"; break;
                case "path118": Name = "Курганская область"; break;
                case "path158": Name = "Свердловская область"; break;
                case "path76": Name = "Пермская область"; break;
                case "path114": Name = "Кировская область"; break;
                case "path54": Name = "Удмуртская республика"; break;
                case "path36": Name = "Республика Коми"; break;
                case "path355": Name = "Ненецкий автономный округ"; break;
                case "path361": Name = "Ямало-Ненецкий автономный округ"; break;
                case "path357": Name = "Ханты-Мансийский автономный округ - Югра"; break;
                case "path170": Name = "Тюменская область"; break;
                case "path138": Name = "Омская область"; break;
                case "path136": Name = "Новосибирская область"; break;
                case "path166": Name = "Томская область"; break;
                case "path66": Name = "Алтайский край"; break;
                case "path19": Name = "Республика Алтай"; break;
                case "path112": Name = "Кемеровская область"; break;
                case "path56": Name = "Республика Хакасия"; break;
                case "path50": Name = "Республика Тыва"; break;
                case "path74": Name = "Красноярский край"; break;
                case "path106": Name = "Иркутская область"; break;
                case "path44": Name = "Республика Саха (Якутия)"; break;
                case "path22": Name = "Республика Бурятия"; break;
                case "path68": Name = "Забайкальский край"; break;
                case "path86": Name = "Амурская область"; break;
                case "path353": Name = "Еврейская автономная область"; break;
                case "path82": Name = "Хабаровский край"; break;
                case "path78": Name = "Приморский край"; break;
                case "path156": Name = "Сахалинская область"; break;
                case "path126": Name = "Магаданская область"; break;
                case "path359": Name = "Чукотская автономная область"; break;
                case "path70": Name = "Камчатский край"; break;
                default: Name = "Неизвестный регион"; break;
            }

            

            SKPath = path;
        }


    }
}

