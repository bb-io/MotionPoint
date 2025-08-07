using Apps.MotionPoint.Handlers.Base;
using Apps.MotionPoint.Models.Enums;
using Apps.MotionPoint.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.MotionPoint.Handlers;

public class SourceLanguageDataHandler(InvocationContext invocationContext, [ActionParameter] LanguageRequest languageRequest) 
    : LanguageBaseDataHandler(invocationContext, languageRequest)
{
    protected override LanguageRole Language => LanguageRole.Source;
}