# Zoho Payments .NET SDK

Official .NET SDK for the Zoho Payments API — supports IN, IN Sandbox, and US editions.

API reference:
- **India:** https://www.zoho.com/in/payments/api/v1/introduction/
- **United States:** https://www.zoho.com/us/payments/api/v1/introduction/

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE)

## Requirements

- **.NET Standard 2.0** or **.NET 8+** (multi-targeted: `netstandard2.0;net8.0`)

## Installation

### .NET CLI

```sh
dotnet add package ZohoPayments
```

### PackageReference

```xml
<ItemGroup>
  <PackageReference Include="ZohoPayments" Version="1.0.1" />
</ItemGroup>
```

## Quick Start

```csharp
using ZohoPayments;
using ZohoPayments.Params;

// 1. Build the client
using var client = ZohoPaymentsClient.Builder()
    .AccountId("23137556")
    .Edition(Edition.IN)
    .OauthToken("1000.xxxx.yyyy")
    .Build();

// 2. Use a service
var link = client.PaymentLinks().Create(new PaymentLinkCreateParams(
    amount: 500.00,
    currency: "INR",
    description: "Order #1234",
    email: "customer@example.com"));

Console.WriteLine($"Created: {link.PaymentLinkId}");

// 3. Client is disposed automatically at the end of the `using` block
```

## Editions

| Edition | Payments API Base URL | OAuth Accounts URL |
|---------|-----------------------|--------------------|
| `Edition.IN` | `https://payments.zoho.in/api/v1` | `https://accounts.zoho.in` |
| `Edition.IN_SANDBOX` | `https://paymentssandbox.zoho.in/api/v1` | `https://accounts.zoho.in` |
| `Edition.US` | `https://payments.zoho.com/api/v1` | `https://accounts.zoho.com` |

Helper extension methods: `edition.IsIn()` returns `true` for both `IN` and `IN_SANDBOX`; `edition.IsUs()` returns `true` for `US`.

## Authentication

### Access token only

```csharp
var client = ZohoPaymentsClient.Builder()
    .AccountId("23137556")
    .Edition(Edition.IN)
    .OauthToken("1000.access_token_here")
    .Build();
```

### Token refresh

The SDK does **not** auto-refresh tokens. Call `ZohoPaymentsClient.GenerateAccessToken()` when the access token expires, then push the new token into the client:

```csharp
using ZohoPayments.Auth;

OAuthToken fresh = ZohoPaymentsClient.GenerateAccessToken(
    refreshToken, clientId, clientSecret, redirectUri, Edition.IN);

// Persist the new token to your storage layer
myStore.Save(fresh.AccessToken, fresh.ExpiresIn);

// Update the running client (thread-safe, no rebuild needed)
client.UpdateToken(fresh.AccessToken);
```

You can also pass an `OAuthToken` directly to the builder:

```csharp
OAuthToken token = ZohoPaymentsClient.GenerateAccessToken(...);
var client = ZohoPaymentsClient.Builder()
    .AccountId("23137556")
    .Edition(Edition.IN)
    .OauthToken(token)
    .Build();
```

`OAuthToken` exposes `AccessToken` and `ExpiresIn` (token lifetime in seconds).

## Client Configuration

```csharp
var client = ZohoPaymentsClient.Builder()
    .AccountId("23137556")                          // Required
    .Edition(Edition.IN)                            // Required
    .OauthToken("1000.xxx...")                      // Required
    .ConnectTimeout(15)                              // TCP connect timeout, seconds (default: 30)
    .RequestTimeout(45)                              // Per-request read timeout, seconds (default: 60)
    .AddDefaultHeader("X-Custom-Header", "value")   // Custom header sent with every request
    .HttpClient(myCustomTransport)                   // Custom HTTP transport (see below)
    .Build();
```

### Custom HTTP transport

Implement `IHttpClient` to use a different HTTP stack or a test mock:

```csharp
using ZohoPayments.Net;

public sealed class MyHttpClient : IHttpClient
{
    public ZohoResponse Execute(ZohoRequest request)
    {
        // Map ZohoRequest to your preferred HTTP library
    }

    public void Close()
    {
        // Release transport resources
    }
}
```

When a custom transport is set, `ConnectTimeout` is ignored (your client controls its own timeouts).

> **Note on timeouts:** `System.Net.Http.HttpClient` does not expose a distinct connect-phase timeout on every target framework. On `net8.0` the default transport uses `SocketsHttpHandler.ConnectTimeout` to honor `ConnectTimeout` precisely; on `netstandard2.0` consumers (older .NET Framework handlers) only the overall `RequestTimeout` is enforced.

## API Usage Examples

### Customers

```csharp
using ZohoPayments.Models;
using ZohoPayments.Params;

// Create (all editions)
var customer = client.Customers().Create(new CustomerCreateParams(
    name: "Jane Doe",
    email: "jane@example.com"));

// Retrieve (all editions)
var c = client.Customers().Get(customer.CustomerId!);

// List (US only — not available on IN edition)
ListResponse<CustomerSummary> customers = client.Customers().List(
    new CustomerListParams(perPage: 10, page: 1));

// Delete (US only — customers with saved payment methods cannot be deleted)
client.Customers().Delete(customer.CustomerId!);
```

### Payment Sessions

```csharp
var session = client.PaymentSessions().Create(new PaymentSessionCreateParams(
    amount: 100.00,
    currency: "INR",
    description: "Checkout #42",
    expiresIn: 600));

var retrieved = client.PaymentSessions().Get(session.PaymentsSessionId!);
```

### Payment Links

```csharp
// Create
var link = client.PaymentLinks().Create(new PaymentLinkCreateParams(
    amount: 500.00,
    currency: "INR",
    description: "Invoice #2025-001",
    email: "buyer@example.com"));

// Update
client.PaymentLinks().Update(link.PaymentLinkId!, new PaymentLinkUpdateParams(
    description: "Invoice #2025-001 (revised)"));

// Cancel
client.PaymentLinks().Cancel(link.PaymentLinkId!);
```

### Payments & Refunds

```csharp
// List payments
ListResponse<PaymentSummary> payments = client.Payments().List();

// Retrieve one payment
var payment = client.Payments().Get("6485000000045015");

// Create a refund (type: "initiated_by_merchant" or "initiated_by_customer")
var refund = client.Refunds().Create("6485000000045015", new RefundCreateParams(
    amount: 50.00,
    reason: "requested_by_customer",
    type: "initiated_by_customer"));

// Retrieve a refund
var r = client.Refunds().Get(refund.RefundId!);
```

### Payouts

```csharp
// List payouts
ListResponse<Payout> payouts = client.Payouts().List(
    new PayoutListParams(status: "paid", perPage: 25));

// Retrieve one payout
var payout = client.Payouts().Get(payouts.Data[0].PayoutId!);
Console.WriteLine(payout.TransactionSummary?.TotalAmount);

// List the transactions settled in a payout
ListResponse<PayoutTransaction> transactions = client.Payouts().ListTransactions(
    payout.PayoutId!,
    new PayoutTransactionListParams(perPage: 50));
```

### Mandates (IN only)

```csharp
var mandates = client.Mandates(); // throws InvalidOperationException on Edition.US

var session = mandates.CreateEnrollmentSession(new MandateEnrollmentSessionCreateParams(
    amount: 100.00,
    currency: "INR",
    customerId: customer.CustomerId!,
    description: "UPI Autopay enrollment",
    mandateDetails: new MandateDetailsParams(
        paymentMethodType: "upi",
        frequency: "monthly",
        description: "Monthly subscription",
        amountRule: "fixed")));
```

### Collect / Virtual Accounts (IN only)

```csharp
var collect = client.Collect(); // throws InvalidOperationException on Edition.US

var account = collect.Create(new VirtualAccountCreateParams(description: "Order #1234"));
collect.Close(account.VirtualAccountId!);
```

### Split Settlement (IN only)

```csharp
var splitSettlement = client.SplitSettlement(); // throws InvalidOperationException on Edition.US

// Onboard a connected account
splitSettlement.CreateConnectedAccount(new ConnectedAccountCreateParams(
    accountName: "Acme Retail",
    emailId: "owner@acme.example",
    pan: "ABCDE1234F",
    mcc: "5399",
    businessDescription: "General merchandise",
    connectedAccountBankAccount: new ConnectedAccountBankAccountParams(
        routingNumber: "HDFC0000123",
        accountNumber: "50100123456789")));

ListResponse<ConnectedAccountSummary> accounts = splitSettlement.ListConnectedAccounts();
var account = splitSettlement.GetConnectedAccount(accounts.Data[0].ConnectedAccountId!);

// Split a payment across connected accounts.
// The request can partially succeed - check each split's own status.
var created = splitSettlement.CreateTransfer(new TransferCreateParams(
    paymentId: "6485000000045015",
    transferSplit: new[]
    {
        new TransferSplitParams(
            connectedAccountId: account.ConnectedAccountId!,
            amount: "250.00",
            description: "Vendor share"),
    }));

foreach (var split in created.Splits!)
{
    Console.WriteLine($"{split.ConnectedAccountId}: {split.Status} {split.ErrorCode}");
}

var transfer = splitSettlement.GetTransfer(created.Splits![0].TransferId!);
ListResponse<TransferSummary> transfers = splitSettlement.ListTransfers(
    new TransferListParams(connectedAccountId: account.ConnectedAccountId!));

// Reverse part of a transfer
var reversal = splitSettlement.CreateTransferReversal(new TransferReversalCreateParams(
    transferId: transfer.TransferId!,
    reversalAmount: "100.00",
    description: "Partial reversal"));

var reversalDetail = splitSettlement.GetTransferReversal(reversal.TransferReversalId!);
ListResponse<TransferReversal> reversals = splitSettlement.ListTransferReversals();

// Connected account ledger and payouts
ListResponse<ConnectedAccountTransaction> ledger =
    splitSettlement.ListConnectedAccountTransactions(
        account.ConnectedAccountId!,
        new ConnectedAccountTransactionListParams(transactionType: "charge"));

ListResponse<ConnectedAccountPayoutSummary> accountPayouts =
    splitSettlement.ListConnectedAccountPayouts(account.ConnectedAccountId!);

var accountPayout = splitSettlement.GetConnectedAccountPayout(
    account.ConnectedAccountId!, accountPayouts.Data[0].PayoutId!);

ListResponse<ConnectedAccountPayoutTransaction> payoutTransactions =
    splitSettlement.ListConnectedAccountPayoutTransactions(
        account.ConnectedAccountId!, accountPayout.PayoutId!);
```

### Payment Methods & Payment Method Sessions (US only)

```csharp
var sessions = client.PaymentMethodSessions(); // throws InvalidOperationException on Edition.IN

var pmSession = sessions.Create(new PaymentMethodSessionCreateParams(customerId: customer.CustomerId!));
var paymentMethod = client.PaymentMethods().Get(pmSession.PaymentMethod!.PaymentMethodId!);
```

## Pagination

List methods return `ListResponse<T>` containing:
- `Data` — the list of items (read-only)
- `PageContext` — page/per-page/total/total-pages/has-more-page metadata

List param classes that accept pagination implement `IPaginationParams` and expose `PerPage`/`Page`.

## Metadata

Several create/update operations accept `IReadOnlyList<MetaDataParams>` — key/value pairs attached to a resource:

```csharp
using ZohoPayments.Params;

var meta = new List<MetaDataParams>
{
    new MetaDataParams("order_id", "ORD-123"),
    new MetaDataParams("source", "mobile-app"),
};
```

On the response side, the same data is exposed as `List<MetaData>` (`ZohoPayments.Models.MetaData`).

**Constraints** (validated client-side): maximum 5 entries, key <= 20 characters, value <= 500 characters.

## Error Handling

SDK errors are split into two families:

- **Caller / programmer errors** (bad arguments you passed to the SDK before any network call) throw standard BCL exceptions: `ArgumentException` for missing/invalid fields, `InvalidOperationException` when a consumed builder is reused or an edition-gated service is accessed on the wrong edition. These indicate bugs in your code and should generally not be caught in normal flow — fix the call site.
- **SDK runtime failures** (transport errors, unexpected responses, or Zoho API errors) are rooted at `ZohoPaymentsException`:

```
Exception
+-- ArgumentException                       -- caller supplied a missing/invalid argument
+-- InvalidOperationException                -- Builder already consumed; edition-gated service misuse
+-- ZohoPaymentsException                    -- base SDK runtime error
    +-- ConnectionException                  -- network / I/O failure
    +-- ZohoPaymentsApiException             -- API returned an error
        +-- AuthenticationException           -- HTTP 401
        +-- PermissionException               -- HTTP 403
        +-- ResourceNotFoundException         -- HTTP 404
        +-- InvalidRequestException           -- HTTP 400 / 422
        +-- RateLimitException                -- HTTP 429
```

### Handling API errors

```csharp
using ZohoPayments.Exceptions;

try
{
    client.Payments().Get("invalid-id");
}
catch (ResourceNotFoundException e)
{
    Console.WriteLine($"Not found: {e.ApiErrorMessage}");
}
catch (AuthenticationException)
{
    Console.WriteLine("Token expired — refresh and retry");
}
catch (RateLimitException)
{
    Console.WriteLine("Throttled — back off and retry");
}
catch (ZohoPaymentsApiException e)
{
    Console.WriteLine($"HTTP {e.HttpStatusCode} code={e.CodeString} message={e.ApiErrorMessage}");
}
catch (ConnectionException e)
{
    Console.WriteLine($"Network error: {e.Message}");
}
```

## Thread Safety

`ZohoPaymentsClient` is safe to share across threads. `UpdateToken()` is atomic and does not require rebuilding the client.

## Disposal

`ZohoPaymentsClient` implements `IDisposable`. Prefer a `using` block/statement; `Dispose()` is idempotent — calling it multiple times is safe, and subsequent API calls after disposal throw.

## License

Apache License, Version 2.0. See [LICENSE](LICENSE).
