using Application.DTOs.Users;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.IO;

namespace Application.Mappings.Actions
{
    public class PersonalDetailsMappingAction : IMappingAction<UserProfile, UserDetailsVM>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IFileUploadService _fileUpload;
        string folderPath = Path.GetFullPath("FileUpload");

        public PersonalDetailsMappingAction(IFileUploadService fileUpload,
            IDateTimeService dateTime)
        {
            //_applicationStages = applicationStages;
            _fileUpload = fileUpload;
            _dateTime = dateTime;
            //_titleRepo = titleRepo;
        }

        public void Process(UserProfile source, UserDetailsVM destination, ResolutionContext context)
        {
        }
    }

}
