﻿// <copyright file="AWSServicesFactory.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
namespace Bellatrix.AWS;
public class AWSServicesFactory
{
    public Rekognition Rekognition => ServicesCollection.Current.Resolve<Rekognition>();
    public S3BucketService S3 => ServicesCollection.Current.Resolve<S3BucketService>();
    public SecretsManager SecretsManager => ServicesCollection.Current.Resolve<SecretsManager>();
    public SecretsResolver SecretsResolver => ServicesCollection.Current.Resolve<SecretsResolver>();
}
