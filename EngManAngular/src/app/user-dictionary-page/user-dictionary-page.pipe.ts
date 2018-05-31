import { Pipe, PipeTransform } from "@angular/core";

@Pipe(
    {
        name: 'searchUserDictionary',
        pure: false
    }
)
export class SearchUserDictionaryPipe implements PipeTransform
{
    transform(words, value)
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