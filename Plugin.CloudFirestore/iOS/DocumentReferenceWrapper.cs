﻿using System;
using System.Threading.Tasks;
using Firebase.CloudFirestore;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace Plugin.CloudFirestore
{
    public class DocumentReferenceWrapper : IDocumentReference, IEquatable<DocumentReferenceWrapper>
    {
        private readonly DocumentReference _documentReference;

        public DocumentReferenceWrapper(DocumentReference documentReference)
        {
            _documentReference = documentReference ?? throw new ArgumentNullException(nameof(documentReference));
        }

        public string Id => _documentReference.Id;

        public string Path => _documentReference.Path;

        public ICollectionReference Parent => new CollectionReferenceWrapper(_documentReference.Parent);

        public IFirestore Firestore => FirestoreProvider.GetFirestore(_documentReference.Firestore);

        public ICollectionReference GetCollection(string collectionPath)
        {
            return Collection(collectionPath);
        }

        public ICollectionReference Collection(string collectionPath)
        {
            var collectionReference = _documentReference.GetCollection(collectionPath);
            return new CollectionReferenceWrapper(collectionReference);
        }

        public void GetDocument(DocumentSnapshotHandler handler)
        {
            _documentReference.GetDocument((snapshot, error) =>
            {
                handler?.Invoke(snapshot == null ? null : new DocumentSnapshotWrapper(snapshot),
                                error == null ? null : ExceptionMapper.Map(error));

            });
        }

        public void GetDocument(Source source, DocumentSnapshotHandler handler)
        {
            _documentReference.GetDocument(source.ToNative(), (snapshot, error) =>
            {
                handler?.Invoke(snapshot == null ? null : new DocumentSnapshotWrapper(snapshot),
                                error == null ? null : ExceptionMapper.Map(error));

            });
        }

        public Task<IDocumentSnapshot> GetDocumentAsync()
        {
            return GetAsync();
        }

        public Task<IDocumentSnapshot> GetAsync()
        {
            var tcs = new TaskCompletionSource<IDocumentSnapshot>();

            _documentReference.GetDocument((snapshot, error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(new DocumentSnapshotWrapper(snapshot!));
                }
            });

            return tcs.Task;
        }

        public Task<IDocumentSnapshot> GetDocumentAsync(Source source)
        {
            return GetAsync(source);
        }

        public Task<IDocumentSnapshot> GetAsync(Source source)
        {
            var tcs = new TaskCompletionSource<IDocumentSnapshot>();

            _documentReference.GetDocument(source.ToNative(), (snapshot, error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(new DocumentSnapshotWrapper(snapshot!));
                }
            });

            return tcs.Task;
        }

        public void SetData(object documentData, CompletionHandler handler)
        {
            _documentReference.SetData(documentData.ToNativeFieldValues<object, NSString>()!, (error) =>
             {
                 handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
             });
        }

        public Task SetDataAsync(object documentData)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.SetData(documentData.ToNativeFieldValues<object, NSString>()!, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public Task SetAsync<T>(T documentData)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.SetData(documentData.ToNativeFieldValues<T, NSString>()!, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public void SetData(object documentData, CompletionHandler handler, params string[] mergeFields)
        {
            _documentReference.SetData(documentData.ToNativeFieldValues<object, NSString>()!, mergeFields, (error) =>
            {
                handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
            });
        }

        public void SetData(object documentData, CompletionHandler handler, params FieldPath[] mergeFields)
        {
            _documentReference.SetData(documentData.ToNativeFieldValues<object, NSString>()!, mergeFields.Select(x => x.ToNative()).ToArray(), (error) =>
            {
                handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
            });
        }

        public Task SetDataAsync(object documentData, params string[] mergeFields)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.SetData(documentData.ToNativeFieldValues<object, NSString>()!, mergeFields, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public Task SetAsync<T>(T documentData, params string[] mergeFields)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.SetData(documentData.ToNativeFieldValues<T, NSString>()!, mergeFields, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public Task SetDataAsync(object documentData, params FieldPath[] mergeFields)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.SetData(documentData.ToNativeFieldValues<object, NSString>()!, mergeFields.Select(x => x.ToNative()).ToArray(), (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public Task SetAsync<T>(T documentData, params FieldPath[] mergeFields)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.SetData(documentData.ToNativeFieldValues<T, NSString>()!, mergeFields.Select(x => x.ToNative()).ToArray(), (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public void SetData(object documentData, bool merge, CompletionHandler handler)
        {
            _documentReference.SetData(documentData.ToNativeFieldValues<object, NSString>()!, merge, (error) =>
            {
                handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
            });
        }

        public Task SetDataAsync(object documentData, bool merge)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.SetData(documentData.ToNativeFieldValues<object, NSString>()!, merge, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public Task SetAsync<T>(T documentData, bool merge)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.SetData(documentData.ToNativeFieldValues<T, NSString>()!, merge, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public void UpdateData(object fields, CompletionHandler handler)
        {
            _documentReference.UpdateData(fields.ToNativeFieldValues<object, NSObject>()!, (error) =>
            {
                handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
            });
        }

        public Task UpdateDataAsync(object fields)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.UpdateData(fields.ToNativeFieldValues<object, NSObject>()!, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public Task UpdateAsync<T>(T fields)
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.UpdateData(fields.ToNativeFieldValues<T, NSObject>()!, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public void UpdateData(string field, object? value, CompletionHandler handler, params object?[] moreFieldsAndValues)
        {
            var fields = Field.CreateFields(field, value, moreFieldsAndValues);

            _documentReference.UpdateData(fields, (error) =>
            {
                handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
            });
        }

        public void UpdateData(FieldPath field, object? value, CompletionHandler handler, params object?[] moreFieldsAndValues)
        {
            var fields = Field.CreateFields(field, value, moreFieldsAndValues);

            _documentReference.UpdateData(fields, (error) =>
            {
                handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
            });
        }

        public Task UpdateDataAsync(string field, object? value, params object?[] moreFieldsAndValues)
        {
            return UpdateAsync(field, value, moreFieldsAndValues);
        }

        public Task UpdateAsync(string field, object? value, params object?[] moreFieldsAndValues)
        {
            var fields = Field.CreateFields(field, value, moreFieldsAndValues);

            var tcs = new TaskCompletionSource<bool>();

            _documentReference.UpdateData(fields, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public Task UpdateDataAsync(FieldPath field, object? value, params object?[] moreFieldsAndValues)
        {
            return UpdateAsync(field, value, moreFieldsAndValues);
        }

        public Task UpdateAsync(FieldPath field, object? value, params object?[] moreFieldsAndValues)
        {
            var fields = Field.CreateFields(field, value, moreFieldsAndValues);

            var tcs = new TaskCompletionSource<bool>();

            _documentReference.UpdateData(fields, (error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public void DeleteDocument(CompletionHandler handler)
        {
            _documentReference.DeleteDocument((error) =>
            {
                handler?.Invoke(error == null ? null : ExceptionMapper.Map(error));
            });
        }

        public Task DeleteDocumentAsync()
        {
            return DeleteAsync();
        }

        public Task DeleteAsync()
        {
            var tcs = new TaskCompletionSource<bool>();

            _documentReference.DeleteDocument((error) =>
            {
                if (error != null)
                {
                    tcs.SetException(ExceptionMapper.Map(error));
                }
                else
                {
                    tcs.SetResult(true);
                }
            });

            return tcs.Task;
        }

        public IListenerRegistration AddSnapshotListener(DocumentSnapshotHandler listener)
        {
            var registration = _documentReference.AddSnapshotListener((snapshot, error) =>
            {
                listener?.Invoke(snapshot == null ? null : new DocumentSnapshotWrapper(snapshot),
                                 error == null ? null : ExceptionMapper.Map(error));
            });

            return new ListenerRegistrationWrapper(registration);
        }

        public IListenerRegistration AddSnapshotListener(bool includeMetadataChanges, DocumentSnapshotHandler listener)
        {
            var registration = _documentReference.AddSnapshotListener(includeMetadataChanges, (snapshot, error) =>
            {
                listener?.Invoke(snapshot == null ? null : new DocumentSnapshotWrapper(snapshot),
                                 error == null ? null : ExceptionMapper.Map(error));
            });

            return new ListenerRegistrationWrapper(registration);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DocumentReferenceWrapper);
        }

        public bool Equals(DocumentReferenceWrapper? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            if (ReferenceEquals(_documentReference, other._documentReference)) return true;
            return _documentReference.Equals(other._documentReference);
        }

        public override int GetHashCode()
        {
            return _documentReference?.GetHashCode() ?? 0;
        }

        DocumentReference IDocumentReference.ToNative()
        {
            return _documentReference;
        }
    }
}
