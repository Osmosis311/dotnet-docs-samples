﻿// Copyright (c) 2019 Google LLC.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.

// [START automl_language_text_classification_create_dataset]

using Google.Cloud.AutoML.V1;
using System;

class AutoMLLanguageTextClassificationCreateDataset
{
    /// <summary>
    ///  Demonstrates using the AutoML client to create a dataset.
    /// </summary>
    /// <param name="projectId">GCP Project ID.</param>
    /// <param name="displayName">The name of the dataset to be created.</param>
    public static void LanguageTextClassificationCreateDataset(string projectId = "YOUR-PROJECT-ID",
        string displayName = "YOUR-DATASET-NAME")
    {
        // Initialize client that will be used to send requests. This client only needs to be created
        // once, and can be reused for multiple requests. After completing all of your requests, call
        // the "close" method on the client to safely clean up any remaining background resources.
        AutoMlClient client = AutoMlClient.Create();

        // A resource that represents Google Cloud Platform location.
        string projectLocation = LocationName.Format(projectId, "us-central1");

        // Specify the classification type
        // Types:
        // MultiLabel: Multiple labels are allowed for one example.
        // MultiClass: At most one label is allowed per example.
        ClassificationType classificationType = ClassificationType.Multilabel;

        // Specify the text classification type for the dataset.
        TextClassificationDatasetMetadata metadata = new
            TextClassificationDatasetMetadata
        {
            ClassificationType = classificationType
        };

        Dataset dataset = new
            Dataset
        {
            DisplayName = displayName,
            TextClassificationDatasetMetadata = metadata
        };

        Dataset createdDataset = client
            .CreateDatasetAsync(projectLocation, dataset).Result.PollUntilCompleted().Result;

        // Display the dataset information.
        Console.WriteLine($"Dataset name: {createdDataset.Name}");
        // To get the dataset id, you have to parse it out of the `name` field. As dataset Ids are
        // required for other methods.
        // Name Form: `projects/{project_id}/locations/{location_id}/datasets/{dataset_id}`
        string[] names = createdDataset.Name.Split("/");
        string datasetId = names[names.Length - 1];
        Console.WriteLine($"Dataset id: {datasetId}");
    }
}

// [END automl_language_text_classification_create_dataset]