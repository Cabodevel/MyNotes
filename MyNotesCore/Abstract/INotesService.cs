﻿using MyNotesCore.Common;
using MyNotesCore.Entities;
using System.Threading.Tasks;

namespace MyNotesCore.Abstract
{
    public interface INotesService
    {
        Task<Result<Note>> AddNote(Note note);
        Task<Result<Note>> UpdateNote(Note note);
        Task<Result<Note>> DeleteNote(int id);
    }
}
