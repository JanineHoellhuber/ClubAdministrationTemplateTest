using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using ClubAdministration.Persistence;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;

namespace ClubAdministration.Wpf.ViewModels
{
    class EditMemberViewModel : BaseViewModel
    {
        public MemberDto _selectedMember;
        public string _firstname;
        public string _lastname;

        public MemberDto SelectedMember
        {
            get => _selectedMember;
            set
            {
                _selectedMember = value;
                OnPropertyChanged(nameof(SelectedMember));
                Validate();
            }
        }

        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(Firstname));
                Validate();
            }
        }
        [Required(ErrorMessage = "Lastname is required") ]
        [MinLength(2, ErrorMessage = "Lastname must be at least two characters long")]
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
                Validate();
            }
        }


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
        public EditMemberViewModel(IWindowController Controller, MemberDto selectedMember) : base(Controller)
        {
            SelectedMember = selectedMember;
            Init();
        }

        private void Init()
        {
            Firstname = SelectedMember.FirstName;
            Lastname = SelectedMember.LastName;
        }
        private ICommand _cmdSave;
        public ICommand CmdSave
        {
            get
            {
                if(_cmdSave == null)
                {
                    _cmdSave = new RelayCommand(
                        execute: async _ =>
                        {
                            using IUnitOfWork uow = new UnitOfWork();
                            Member memberInDB = await uow.MemberSectionRepository.GetMemberByIdAsync(_selectedMember.Id);
                            memberInDB.FirstName = Firstname;
                            memberInDB.LastName = Lastname;
                            uow.MemberSectionRepository.Update(memberInDB);
                            await uow.SaveChangesAsync();
                            Controller.CloseWindow(this);
                        },
                        canExecute: _ => _selectedMember != null);
                }
                return _cmdSave;
            }
            set { _cmdSave = value; }
        }

        private ICommand _cmdUndo;

        public ICommand CmdUndo
        {
            get
            {
                if (_cmdUndo == null)
                {
                    _cmdUndo = new RelayCommand(
                      execute: _ =>
                      {
                          Init();

                      },
                      canExecute: _ => true);

                }
                return _cmdUndo;
            }
        }

    }
}
