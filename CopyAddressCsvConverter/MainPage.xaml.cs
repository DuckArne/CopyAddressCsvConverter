using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CopyAddressCsvConverter
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string filText;
        Windows.Storage.StorageFile file;
        public MainPage()
        {
            this.InitializeComponent();
        }

        async private void VäljfilButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".csv");


            file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                valdFilTextblock.Text = "Vald fil: " + file.Name;
               

                try
                {
                    
                    filText = await Windows.Storage.FileIO.ReadTextAsync(file,Windows.Storage.Streams.UnicodeEncoding.Utf8);
                }
                catch (Exception exc)
                {
                    var dialog = new MessageDialog(exc.Message);
                    await dialog.ShowAsync();
                    valdFilTextblock.Text = "Ingen vald fil";
                    file = null;
                }

            }
            else
            {
                valdFilTextblock.Text = "Ingen vald fil";
                file = null;
            }
        }









        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void ValdFilTextblock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

       async private void KonverteraButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (file != null && !string.IsNullOrEmpty(filText))
                {
                    StringBuilder sb = OrganiseraBlahaTxtFil();

                    await SkapaCSVFil(sb);
                }
            }
            catch (Exception exc)
            {
                var dialog = new MessageDialog(exc.Message);
                await dialog.ShowAsync();
                valdFilTextblock.Text = "Ingen vald fil";
                file = null;

            }


        }

        private async System.Threading.Tasks.Task SkapaCSVFil(StringBuilder sb)
        {
            var namnAdress = sb.ToString().Split(',');
            int antalAdresser = namnAdress.Count() / 2;
            List<Adress> adresser = new List<Adress>();
            for (int i = 0; i < antalAdresser; i++)
            {
                var adress = new Adress();
                adress.Visa = namnAdress[i];
                i++;
                adress.E_post = namnAdress[i];
                adresser.Add(adress);
            }

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("CSV fil", new List<string>() { ".csv" });
            savePicker.SuggestedFileName = "Konverterade_Adresser";


            Windows.Storage.StorageFile saveFile = await savePicker.PickSaveFileAsync();
            if (saveFile != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(saveFile);

                using (var writer = new StreamWriter(await saveFile.OpenStreamForWriteAsync()))
                using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(adresser);
                }

                Windows.Storage.Provider.FileUpdateStatus status =
                        await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(saveFile);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    valdFilTextblock.Text = "Filen " + saveFile.Name + " sparades.";
                }
                else
                {
                    valdFilTextblock.Text = "Filen " + saveFile.Name + " kunde inte sparas";
                }
            }
            else
            {
                valdFilTextblock.Text = "Ingen fil vald";
            }
        }

        private StringBuilder OrganiseraBlahaTxtFil()
        {
            var sb = new StringBuilder();

            var adresserMedNamn = filText.Split(',');
            foreach (var post in adresserMedNamn)
            {
                string extraheradNamnAdress;

                var na = post.Replace("\"", "").Replace("\'", "").Trim();
                var indexOfPointChar1 = na.IndexOf('<');
                var indexOfPointChar2 = na.IndexOf('>');

                if (indexOfPointChar1 == -1)
                {
                    //< tecknet finns inte. post består då enbart av en adress.                       
                    extraheradNamnAdress = na + "," + na + ",";
                }
                else
                {
                    //post består av namn och adress inom <> tecknen

                    var adress = na.Substring(indexOfPointChar1 + 1, indexOfPointChar2 - indexOfPointChar1 - 1).Trim();
                    var namn = na.Substring(0, indexOfPointChar1).Replace(',', ' ').Trim();
                    
                    if (String.IsNullOrEmpty(namn))
                    {
                        extraheradNamnAdress = $"{adress},{adress},";
                    }
                    else
                    {
                        extraheradNamnAdress = $"{namn},{adress},";
                    }
                }

                sb.Append(extraheradNamnAdress);
            }

            return sb;
        }
    }
}
