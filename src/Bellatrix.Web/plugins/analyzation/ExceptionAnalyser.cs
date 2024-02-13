// <copyright file="ExceptionAnalyser.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using Bellatrix.ExceptionAnalysation.Contracts;
using Bellatrix.Web;

namespace Bellatrix.ExceptionAnalysation;

public class ExceptionAnalyser : IExceptionAnalyser
{
    private readonly List<IExceptionAnalysationHandler> _exceptionAnalysationHandlers;

    public ExceptionAnalyser() => _exceptionAnalysationHandlers = new List<IExceptionAnalysationHandler>();

    public void RemoveFirstExceptionAnalysationHandler()
    {
        if (_exceptionAnalysationHandlers.Count > 0)
        {
            _exceptionAnalysationHandlers.RemoveAt(0);
        }
    }

    public void Analyse(Exception ex = null, ServicesCollection container = null, params object[] context)
    {
        container ??= ServicesCollection.Current;

        IEnumerable<IExceptionAnalysationHandler> handlers = ServicesCollection.Current.ResolveAll<IExceptionAnalysationHandler>();
        _exceptionAnalysationHandlers.AddRange(handlers);
        foreach (IExceptionAnalysationHandler exceptionHandler in _exceptionAnalysationHandlers)
        {
            if (exceptionHandler.IsApplicable(ex, container, context))
            {
                BrowserService browserService = container?.Resolve<BrowserService>();
                if (browserService != null)
                {
                    string url = browserService.Url.ToString();
                    browserService.PrintConsoleOutput();

                    throw new AnalyzedTestException(exceptionHandler.DetailedIssueExplanation, url, ex);
                }
                else
                {
                    throw new AnalyzedTestException(exceptionHandler.DetailedIssueExplanation);
                }
            }
        }
    }

    public void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>(
        IExceptionAnalysationHandler exceptionAnalysationHandler)
        where TExceptionAnalysationHandler : IExceptionAnalysationHandler => _exceptionAnalysationHandlers.Insert(0, exceptionAnalysationHandler);

    public void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>()
        where TExceptionAnalysationHandler : IExceptionAnalysationHandler, new() => _exceptionAnalysationHandlers.Insert(0, new TExceptionAnalysationHandler());
}