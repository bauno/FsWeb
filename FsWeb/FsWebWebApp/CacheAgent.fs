﻿namespace FsWeb.Repositories

module CacheAgent =
    type private Message =
    | Get of string * AsyncReplyChannel<obj option>
    | Set of string * obj

    let private agent = MailboxProcessor.Start(fun inbox -> 
        let rec loop(cacheMap:Map<string,obj>) =
            async {
                let! message = inbox.Receive()
                match message with 
                | Get(key, replyChannel) -> 
                    Map.tryFind key cacheMap |> replyChannel.Reply
                | Set(key, data) -> 
                    do! loop((key, data) |> cacheMap.Add)
                do! loop cacheMap
                }
        loop Map.empty)

