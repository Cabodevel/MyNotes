using MyNotesCore.Abstract;
using MyNotesCore.Common;
using MyNotesCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesCore.Services
{
    public class NotesService : INotesService
    {
        private readonly IRepository<Note> _notesRepository;

        public NotesService(IRepository<Note> notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<Result<Note>> AddNote(Note note)
        {
            Result<Note> result = await _notesRepository.Create(note);
            return result;
        }

        public Task<Result<Note>> DeleteNote(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ICollection<Note>>> GetAllNotes()
        {
            ICollection<Note> notes = await _notesRepository.GetAll();
            if (notes != null && notes.Any())
                return new Result<ICollection<Note>>(true, notes.OrderBy(x => x.Priority).ToList());
            return new Result<ICollection<Note>>(false, "There aren't any note");
        }

        public Task<Result<Note>> UpdateNote(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
