using MyNotesCore.Abstract;
using MyNotesCore.Common;
using MyNotesCore.Entities;
using System;
using System.Threading.Tasks;

namespace MyNotesCore.Services
{
    public class NotesService : INotesService
    {
        public Task<Result<Note>> AddNote(Note note)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Note>> DeleteNote(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Note>> UpdateNote(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
