﻿namespace Modules.BaseApplication.Pipelines.Authorization;

public interface ISecuredRequest
{
    public string[] Roles { get; }
}