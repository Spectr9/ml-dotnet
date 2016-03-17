open System
open System.IO

type DocType = 
    | Ham
    | Spam

let parseDoc (label:string) =
    match label with
    | "ham"  -> Ham
    | "spam" -> Spam
    | _      -> failwith "Unknown label"

let parseLine (line:string) =
    let split = line.Split('\t')
    (split.[0] |> parseDoc, split.[1])

let path = __SOURCE_DIRECTORY__ + @"..\..\Data\SMSSpamCollection"

let dataset =
    File.ReadAllLines path
    |> Array.map parseLine


// defining simplest tokenizer

open System.Text.RegularExpressions

let matchWords = Regex(@"\w+")

let wordTokenizer (text:string) = 
    text.ToLowerInvariant()
    |> matchWords.Matches
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Value)
    |> Set.ofSeq

#load "NaiveBayes.fs"
open NaiveBayes.Classifier

let validation, training = dataset.[..999], dataset.[1000..]

// Use every single token
let tokenized = 
    training 
    |> Seq.map (fun (lbl,sms) -> lbl,wordTokenizer sms)

let allTokens = 
    training
    |> Seq.map snd
    |> vocabulary wordTokenizer

// generic model evaluation function
let evaluate (tokenizer:Tokenizer) (tokens:Token Set) = 
    let model = train training tokenizer tokens
    validation 
    |> Seq.averageBy (fun (l,o) -> 
        if l = model o then 1. else 0.)
    |> printfn "Correctly classified: %.3f"

evaluate wordTokenizer allTokens


// How many ham we are have?
let hamCnt = tokenized |> Seq.filter (fun (lbl,_) -> lbl=Ham) |> Seq.length
let totalCnt = tokenized |> Seq.length

proportion hamCnt totalCnt

// does casing matter?
let casedTokenizer (text:string) = 
    text
    |> matchWords.Matches
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Value)
    |> Set.ofSeq

let casedTokens = 
    training 
    |> Seq.map snd 
    |> vocabulary casedTokenizer

evaluate casedTokenizer casedTokens

// most frequent tokens

let top n (tokenizer:Tokenizer) (docs:string []) = 
    let tokenized = docs |> Array.map tokenizer
    let tokens = tokenized |> Set.unionMany
    tokens 
    |> Seq.sortBy (fun t -> - countIn tokenized t)
    |> Seq.take n
    |> Set.ofSeq

let ham,spam = 
    let rawHam,rawSpam =
        training 
        |> Array.partition (fun (lbl,_) -> lbl=Ham)
    rawHam |> Array.map snd, 
    rawSpam |> Array.map snd

let hamCount = ham |> vocabulary casedTokenizer |> Set.count
let spamCount = spam |> vocabulary casedTokenizer |> Set.count

let topHam = ham |> top (hamCount / 10) casedTokenizer 
let topSpam = spam |> top (spamCount / 10) casedTokenizer 

let topTokens = Set.union topHam topSpam


evaluate casedTokenizer topTokens


ham |> top 20 casedTokenizer |> Seq.iter (printfn "%s")
spam |> top 20 casedTokenizer |> Seq.iter (printfn "%s")


// identifying most specific tokens

let commonTokens = Set.intersect topHam topSpam
let specificTokens = Set.difference topTokens commonTokens

evaluate casedTokenizer specificTokens