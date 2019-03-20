using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        public List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        public List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        private ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
            //CreateSampleData();

            WireUpLists();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Tim", LastName = "Corey" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Bill", LastName = "Jones" });
        }

        public void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

        }

        private void firstNameValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void teamNameValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {

            /*for (int i = 0; i >= teamMembersListBox.Height; i++)
            {
                teamMembers.Add(teamMembersListBox.GetItemText(teamMembersListBox.GetItemHeight(i)));
            }*/

            if (ValidateForm())
            {
                PersonModel p = new PersonModel(
                    firstNameValue.Text,
                    lastNameValue.Text,
                    emailValue.Text,
                    cellphoneValue.Text);
                
                GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();

                //selectTeamMemberDropDown.Items.Add(p.FirstName + " " + p.LastName);

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill in all of the fields.");
            }
           
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists(); 
            }
            

            /*if (ValidateAddMember())
            {
                teamMembersListBox.Items.Add(selectTeamMemberDropDown.SelectedItem);
                foreach (PersonModel model in availableTeamMembers)
                {
                    if ((model.FirstName + " " + model.LastName).Equals(teamMembersListBox.SelectedItem))
                    {
                        availableTeamMembers.Remove(model);
                        selectedTeamMembers.Add(model);
                        selectTeamMemberDropDown.Items.Remove(selectTeamMemberDropDown.SelectedItem);
                    }
                }
            }*/
        }

        private void deleteSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }

            /*if (teamMembersListBox.SelectedItem != null)
            {
                foreach (PersonModel model in selectedTeamMembers){
                    if((model.FirstName + " " + model.LastName).Equals(teamMembersListBox.SelectedItem))
                    {
                        selectedTeamMembers.Remove(model);
                        availableTeamMembers.Add(model);
                    }
                }
                selectTeamMemberDropDown.Items.Add(teamMembersListBox.SelectedItem);
                teamMembersListBox.Items.Remove(teamMembersListBox.SelectedItem);

            }*/
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel(
                teamNameValue.Text,
                selectedTeamMembers);

            GlobalConfig.Connection.CreateTeam(t);

            callingForm.TeamComplete(t);

            this.Close();
        
            
            /*if (ValidateForm() == true)
            {
                TeamModel model = new TeamModel(
                teamNameValue.Text, 
                selectedTeamMembers);

                GlobalConfig.Connection.CreateTeam(model);
            }
            else
            {
                MessageBox.Show("You need to fill in all of the fields.");
            }*/
        }

        private bool ValidateForm()
        {
            bool output = true;

            if (firstNameValue.Text.Length == 0)
            {
                output = false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                output = false;
            }

            if (emailValue.Text.Length == 0)
            {
                output = false;
            }

            if (cellphoneValue.Text.Length == 0)
            {
                output = false;
            }

            /*if (teamNameValue.Text != null && teamNameValue.Text.Length >= 50)
            {
                output = false;
            }

            if(selectTeamMemberDropDown.Text == null)
            {
                output = false;
            }

            if(teamMembersListBox.ItemHeight == 0)
            {
                output = false;
            }*/

            return output;
        }

        private bool ValidateAddMember()
        {
            bool output = true;

            if (selectTeamMemberDropDown.Text == null)
            {
                output = false;
            }

            return output;
        }

        private bool ValidateCreateMember()
        {
            bool output = true;

            if(firstNameValue.Text.Length >= 50)
            {
                output = false;
            }

            if(lastNameValue.Text.Length >= 50)
            {
                output = false;
            }

            if(emailValue.Text.Length >= 100 || !emailValue.Text.Contains("@") || !emailValue.Text.Contains("."))
            {
                output = false;
            }
            
            if (cellphoneValue.Text.Length >= 20){
                output = false;
            }


            return output;

        }

        private bool ValidateDeleteSelected()
        {
            bool output = true;

            if(teamMembersListBox.SelectedItem == null)
            {
                output = false;
            }

            return output;
        }


    }
}
