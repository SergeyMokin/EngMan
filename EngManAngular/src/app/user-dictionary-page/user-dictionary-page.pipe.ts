import { Pipe, PipeTransform } from "@angular/core";
import { WordModel } from "../app.models";

@Pipe(
    {
        name: 'searchUserDictionary',
        pure: false
    }
)
export class SearchUserDictionaryPipe implements PipeTransform
{
    public transform(words: WordModel[], value: string): WordModel[]
    {
        if(words === undefined)
        {
            return [];
        }
        return words.filter(word => 
            {
                let wordToSearch = word.Translate + " " + word.Original;
                return wordToSearch.includes(value.toLowerCase());
            }
        )
    }
}