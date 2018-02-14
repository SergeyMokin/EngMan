import Login from '../src/components/Login'
import Rules from '../src/components/Rules'
import Trainings from '../src/components/Trainings'
import Grammar from '../src/components/Grammar'
import SentenceTask from '../src/components/SentenceTask'
import WordMapTask from '../src/components/WordMap'
import User from '../src/components/User'
import AdminRule from '../src/components/AdminRule'
import AdminSentence from '../src/components/AdminSentence'
import AdminWord from '../src/components/AdminWord'
import AdminUser from '../src/components/AdminUser'
import AdminGuessesTheImage from '../src/components/AdminGuessesTheImage'
import GuessesTheImages from '../src/components/GuessesTheImages'
import UserDictionary from '../src/components/UserDictionary'

const routes = [
    { path: '/', component: Login }
    , { path: '/user', component: User }
    , { path: '/trainings', component: Trainings }
    , { path: '/grammar', component: Grammar }
    , { path: '/dictionary', component: UserDictionary }
    , { path: '/grammar/rules', component: Rules }
    , { path: '/grammar/sentencetask', component: SentenceTask }
    , { path: '/trainings/wordmap', component: WordMapTask }
    , { path: '/trainings/guessestheimages', component: GuessesTheImages }
    , { path: '/admin/rules', component: AdminRule }
    , { path: '/admin/sentences', component: AdminSentence }
    , { path: '/admin/words', component: AdminWord }
    , { path: '/admin/users', component: AdminUser }
    , { path: '/admin/guessestheimages', component: AdminGuessesTheImage }
]

export default routes