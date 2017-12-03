using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PredictiveSystemManagement
{
    public partial class MainForm : Form
    {
        List<FileDialog> listOfCsvFiles = new List<FileDialog>();
        public MainForm()
        {
            InitializeComponent();
            SetAppearance();
        }

        private void SetAppearance()
        {
            //HistoricalDataPanel
            HistoricalDataPanel.Size = new Size(Constants.WidthHD, Constants.HeightHD);

            HistoricalDataGroupBox.Size = new Size(Constants.WidthGroupBoxHD, Constants.HeightGroupBoxHD);
            HistoricalDataGroupBox.Location = new Point(
                (HistoricalDataPanel.Width - HistoricalDataGroupBox.Width) / 2,
                (HistoricalDataPanel.Height - HistoricalDataGroupBox.Height) / 2);

            FilesListLabel.Location = new Point(Constants.MarginLeftLabelHD, Constants.MarginTopLabelHD);

            FilesListBox.Size = new Size(Constants.WidthFilesListBoxHD, Constants.HeightFilesListBoxHD);
            FilesListBox.Location = new Point(Constants.MarginLeftFilesListBoxHD, Constants.MarginTopFilesListBoxHD);

            ProcessFilesButton.Size = new Size(Constants.WidthProcessFilesButtonHD, Constants.HeightProcessFilesButtonHD);
            ProcessFilesButton.Location = new Point(Constants.MarginLeftProcessFilesButtonHD, Constants.MarginTopProcessFilesButtonHD);

            AddFileButton.Size = new Size(Constants.WidthAddFileButtonHD, Constants.HeightAddFileButtonHD);
            AddFileButton.Location = new Point(Constants.MarginLeftAddFileButtonHD, Constants.MarginTopAddFileButtonHD);

            RemoveFileButton.Size = new Size(Constants.WidthRemoveFileButtonHD, Constants.HeightRemoveFileButtonHD);
            RemoveFileButton.Location = new Point(Constants.MarginLeftRemoveFileButtonHD, Constants.MarginTopRemoveFileButtonHD);

            //////////////////////////////////////////////////////////////////////
            //ManagementSystemPanel
            ManagementSystemPanel.Size = new Size(Constants.WidthMS, Constants.HeightMS);

            ManagementSystemGroupBox.Size = new Size(Constants.WidthGroupBoxMS, Constants.HeightGroupBoxMS);
            ManagementSystemGroupBox.Location = new Point(
                (ManagementSystemPanel.Width - ManagementSystemGroupBox.Width) / 2,
                (ManagementSystemPanel.Height - ManagementSystemGroupBox.Height) / 2);

            //////////////////////////////////////////////////////////////////////
        }

        enum TableNames {Matches, Teams, RealScores, PredictedScores, Players, Players4Match};

        private bool CheckCsvFileInList(FileDialog file)
        {
            foreach (FileDialog listElement in listOfCsvFiles)
            {
                if (file.FileName == listElement.FileName)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Team> GetTeamsFromDB()
        {
            List<Team> listOfTeams = new List<Team>();
            try
            {
                SqlDataReader sqlReader = null;
                SqlConnection connection = new SqlConnection("Data Source=.;Database=" + Constants.NameOfDatabase + ";Integrated Security=True;");
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TEAMS", connection);
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    int id = (sqlReader["Id"] as int?) ?? 0;
                    string name = sqlReader["Name"] as string;
                    Team newTeam = new Team(id, name);
                    listOfTeams.Add(newTeam);
                }
                connection.Close();
                //foreach (Team team in listOfTeam)
                //{
                //    MessageBox.Show(team.Id.ToString() + "name: " + team.Name);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: GetTeamsFromDB()\n" + e.Message);
            }

            return listOfTeams;
        }

        private List<Match> GetMatchesFromDB()
        {
            List<Match> listOfMatches = new List<Match>();
            try
            {
                SqlDataReader sqlReader = null;
                SqlConnection connection = new SqlConnection("Data Source=.;Database=" + Constants.NameOfDatabase + ";Integrated Security=True;");
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM MATCHES", connection);
                sqlReader = command.ExecuteReader();
                
                while (sqlReader.Read())
                {
                    int id = (sqlReader["Id"] as int?) ?? 0;
                    int homeId = (sqlReader["HomeId"] as int?) ?? 0;
                    int awayId = (sqlReader["AwayId"] as int?) ?? 0;
                    string date = sqlReader["Date"] as string;
                    int predictedScoreId = (sqlReader["PredictedScoreId"] as int?) ?? 0;
                    int realScoreId = (sqlReader["RealScoreId"] as int?) ?? 0;
                    string referee = sqlReader["Referee"] as string;
                    Match newMatch = new Match(id, homeId, awayId, date, predictedScoreId, realScoreId, referee);
                    listOfMatches.Add(newMatch);
                }
                connection.Close();
                //foreach (Match match in listOfMatches)
                //{
                //    MessageBox.Show(match.Id.ToString() + " : " + match.HomeId.ToString() + " : " + match.AwayId.ToString() + " - " +
                //        "date: " + match.Date + " - " + match.PredictedScoreId.ToString() + " : " + match.RealScoreId.ToString() + " referee " + match.Referee);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: GetMatchesFromDB()\n" + e.Message);
            }

            return listOfMatches;
        }

        private int GetNumberOfXFromDB(string tableName)
        {
            int number = 0;
            try
            {
                SqlDataReader sqlReader = null;
                SqlConnection connection = new SqlConnection("Data Source=.;Database=" + Constants.NameOfDatabase + ";Integrated Security=True;");
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM " + tableName, connection);
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    number++;
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: GetNumberOfXFromDB(string tableName)\n" + e.Message);
            }

            return number;
        }

        private void ExecuteQuery(string query)
        {
            try
            { 
                SqlConnection connection = new SqlConnection("Data Source=.;Database=" + Constants.NameOfDatabase + ";Integrated Security=True;");
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ExecuteQuery(string query)\n" + e.Message);
            }
        }

        private void CreateQueryFromCsv(FileDialog file)
        {
            try
            {
                using (TextReader reader = File.OpenText(file.FileName))
                {
                    List<Team> listOfTeams = GetTeamsFromDB();
                    int numberOfRealScores = GetNumberOfXFromDB(TableNames.RealScores.ToString());
                    int numberOfMatches = GetNumberOfXFromDB(TableNames.Matches.ToString());
                    string query = "";

                    var csv = new CsvReader(reader);
                    while (csv.Read())
                    {
                        var date = csv.GetField<String>("Date");
                        var referee = csv.GetField<String>("Referee");
                        var homeTeam = csv.GetField<String>("HomeTeam");
                        var awayTeam = csv.GetField<String>("AwayTeam");
                        var homeGoals = csv.GetField<String>("FTHG");
                        var awayGoals = csv.GetField<String>("FTAG");
                        var homeShots = csv.GetField<String>("HS");
                        var awayShots = csv.GetField<String>("AS");
                        var homeShotsOnTarget = csv.GetField<String>("HST");
                        var awayShotsOnTarget = csv.GetField<String>("AST");
                        var homeCorners = csv.GetField<String>("HC");
                        var awayCorners = csv.GetField<String>("AC");
                        // var homeOffsides = csv.GetField<String>("HO");
                        // var awayOffsides = csv.GetField<String>("AO");
                        var homeFouls = csv.GetField<String>("HF");
                        var awayFouls = csv.GetField<String>("AF");
                        var homeYellowCards = csv.GetField<String>("HY");
                        var awayYellowCards = csv.GetField<String>("AY");
                        var homeRedCards = csv.GetField<String>("HR");
                        var awayRedCards = csv.GetField<String>("AR");
                        
                        if (!listOfTeams.Exists(x => x.Name == homeTeam))
                        {
                            Team newTeam = new Team(listOfTeams.Count + 1, homeTeam);
                            listOfTeams.Add(newTeam);
                            query += "INSERT INTO TEAMS VALUES(" + newTeam.Id.ToString() + ", '" + newTeam.Name + "');\n";
                        }

                        if (!listOfTeams.Exists(x => x.Name == awayTeam))
                        {
                            Team newTeam = new Team(listOfTeams.Count + 1, awayTeam);
                            listOfTeams.Add(newTeam);
                            query += "INSERT INTO TEAMS VALUES(" + newTeam.Id.ToString() + ", '" + newTeam.Name + "');\n";
                        }

                        int homeId = listOfTeams.Find(x => x.Name == homeTeam).Id;
                        int awayId = listOfTeams.Find(x => x.Name == awayTeam).Id;

                        /// Change date: DD/MM/YY -> MM/DD/YY (CSV -> SQL) ///
                        string str = date.Substring(0, 3);
                        date = date.Remove(0, 3).Insert(3, str);
                        /////////////////////////////////////////////////////

                        query += "INSERT INTO REALSCORES VALUES(" + (++numberOfRealScores).ToString() + ", " + homeGoals + ", "
                            + awayGoals + ", " + homeShots + ", " + awayShots + ", " + homeShotsOnTarget + ", " + awayShotsOnTarget + ", "
                            + homeCorners + ", " + awayCorners + ", " + homeFouls + ", " + awayFouls + ", " + homeYellowCards + ", "
                            + awayYellowCards + ", " + homeRedCards + ", " + awayRedCards + ");\n";
                        
                        query += "INSERT INTO MATCHES VALUES(" + (++numberOfMatches).ToString() + ", " + homeId.ToString() + ", " 
                            + awayId.ToString() + ", '" + date + "', null, " + numberOfRealScores.ToString() + ", '" + referee + "');\n";
                        
                    }

                    //MessageBox.Show(query);
                    ExecuteQuery(query);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Wybrany plik: " + file.FileName + "\nzawiera niekompletne dane. Upewnij się, że wybrano prawidłowy plik.", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error ProcessCsvFile: " + file.FileName + "\n" + e.Message);
            }
        }

        private void AddFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog newFileDialog = new OpenFileDialog();
            newFileDialog.Title = "Wybierz plik CSV z wynikami";
            newFileDialog.InitialDirectory = Application.StartupPath;
            newFileDialog.Filter = "Pliki CSV|*.csv";

            if (newFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (CheckCsvFileInList(newFileDialog))
                {
                    listOfCsvFiles.Add(newFileDialog);
                    FilesListBox.Items.Add(Path.GetFileName(newFileDialog.FileName));
                }
                else
                {
                    MessageBox.Show("Wybrany plik znajduje się już na liście plików.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RemoveFileButton_Click(object sender, EventArgs e) 
        {
            var selectedFile = FilesListBox.SelectedIndex;
            if (selectedFile.ToString() != "-1")
            {
                listOfCsvFiles.RemoveAt(selectedFile);
                FilesListBox.Items.RemoveAt(selectedFile);
            }
            else
            {
                MessageBox.Show("W celu usunięcia pliku z listy, należy go zaznaczyć.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ProcessFilesButton_Click(object sender, EventArgs e)
        {
            if (listOfCsvFiles.Count > 0)
            {
                string information = "Przetwarzanie plików:\n";
                foreach (FileDialog listElement in listOfCsvFiles)
                {
                    CreateQueryFromCsv(listElement);
                    information += (">" + Path.GetFileName(listElement.FileName) + "\n");
                }
                MessageBox.Show(information + "zakończone sukcesem. Dane zostały dodane do bazy danych: " + Constants.NameOfDatabase, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Nie wybrano żadnych plików. W celu przetworzenia danych historycznych, należy wybrać odpowiednie pliki z dysku.", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

    }
}
