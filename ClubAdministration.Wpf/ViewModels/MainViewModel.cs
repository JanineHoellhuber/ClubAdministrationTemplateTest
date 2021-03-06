﻿using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using ClubAdministration.Persistence;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using ClubAdministration.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClubAdministration.Wpf.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
        public ObservableCollection<Section> _section;
        public ObservableCollection<MemberDto> _members;
        public MemberDto selectedMember;
        public Section selectedSection;
        public MemberDto selectedMemberNow;

        public ObservableCollection<Section> Sections
        {
            get => _section;
            set
            {
                _section = value;
                OnPropertyChanged(nameof(Sections));
            }

        }
        public ObservableCollection<MemberDto> Members
        {
            get => _members;
            set
            {
                _members = value;
                OnPropertyChanged(nameof(Members));
            }

        }
        public Section SelectedSection
        {
            get => selectedSection;
            set
            {
                selectedSection = value;
                OnPropertyChanged(nameof(SelectedSection));
                LoadMembers();
            }

        }
        public MemberDto SelectedMember
        {
            get => selectedMember;
            set
            {
                selectedMember = value;
                OnPropertyChanged(nameof(SelectedMember));
            }

        }
   public MainViewModel(IWindowController windowController) : base(windowController)
    {
      LoadCommands();
    }

    private void LoadCommands()
    {
    }

    private async Task LoadDataAsync()
    {
            using IUnitOfWork uow = new UnitOfWork();
            var sections = await uow.SectionRepository
                .GetAllAsync();
            Sections = new ObservableCollection<Section>(sections);
            selectedSection = Sections.First();
            await LoadMembers();
       

    }

        private async Task LoadMembers()
        {
            selectedMemberNow = SelectedMember;
            using IUnitOfWork uow = new UnitOfWork();
            var members = await uow.MemberSectionRepository
                .GetMembersBySection(SelectedSection.Id);
            Members = new ObservableCollection<MemberDto>(members);
            if(selectedMemberNow == null)
            {
                SelectedMember = Members.First();
            }
            else
            {
                SelectedMember = selectedMember;
            }

        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      throw new NotImplementedException();
    }

    public static async Task<MainViewModel> CreateAsync(IWindowController windowController)
    {
      var viewModel = new MainViewModel(windowController);
      await viewModel.LoadDataAsync();
      return viewModel;
    }

        private ICommand _cmdEditMember;
        public ICommand CmdEditMember
        {
            get
            {
                if(_cmdEditMember == null)
                {
                    _cmdEditMember = new RelayCommand(
                        execute: _ =>
                       {
                           Controller.ShowWindow(new EditMemberViewModel(Controller, SelectedMember), true);
                           LoadDataAsync();
                       },
                        canExecute: _ => SelectedMember != null);
                }
                return _cmdEditMember;
            }
        }
    }
}
