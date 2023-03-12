﻿using InstaQ.Application.Abstractions.InstagramRequests.DTOs;

namespace InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;

public interface ICommentsService
{
    Task<CommentsResultDto> GetAsync(string id, int count, CancellationToken token);
}