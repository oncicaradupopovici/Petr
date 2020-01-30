namespace Petr.Tracking.Domain

open System

module Shared =
    type TrackingMonthId = TrackingMonthId of int
    type TrackingMonth = {
        TrackingMonthId: int
        Name: string
        StartDate: DateTime
        EndDate: DateTime
    }

module TransactionAggregate = 
    type TransactionId = TransactionId of Guid
    type AccountNo = AccountNo of string

    type BankAccount = {
        AccountNo: AccountNo
        Owner: string
        Bank: string
    }

    type BankTransferInfo = {
        Account: BankAccount
        Details: string
    }

    type CashWithdrawalInfo = {
        CashTerminal: string
    }

    type PosPaymentInfo = {
        CardNo: string
        PosTernimal: string
    }

    type DirectDebitInfo = {
        DirectDebitCode: string
        Details: string
    }

    type IncomeTransferInfo = {
        Account: BankAccount
        Details: string
    }

    type CashDepozitInfo = {
        CashTerminal: string
    }

    type DebitTransactionMethod = 
        | BankTransfer of BankTransferInfo
        | CashWithdrawal of CashWithdrawalInfo
        | PosPayment of PosPaymentInfo
        | DirectDebit of DirectDebitInfo

    type CreditTransactionMethod = 
        | IncomeTransfer of BankTransferInfo
        | CashDepozit of CashDepozitInfo

    type TransactionType = 
        | Debit of DebitTransactionMethod
        | Credit of CreditTransactionMethod

    type TransactionData = {
        Account: AccountNo
        Amount: decimal
        Date: DateTime
    }
    
    type Transaction = {
        TransactionId: TransactionId
        Data: TransactionData
        Type: TransactionType
        Month: Shared.TrackingMonthId
    }

    type DomainEvent = 
        | TransactionAdded of Transaction
        | BankAccountAdded of BankAccount

    type AddBankTransfer = TransactionData -> BankTransferInfo -> Transaction
    let addBankTransfer: AddBankTransfer =
        fun transactionData bankTransfer ->
            {
                TransactionId = TransactionId (Guid.NewGuid())
                Data = transactionData
                Type = Debit (BankTransfer bankTransfer)
                Month = Shared.TrackingMonthId 1
            }
    
