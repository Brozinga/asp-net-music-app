﻿namespace MusicApp.Domain.ViewModels.Responses
{
    public class BasicObject
    {
        public string Title { get; set; }
        public object Message { get; set; }

        public BasicObject(string title, object message)
        {
            Title = title;
            Message = message;
        }
    }
}