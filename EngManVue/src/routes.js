import Login from '../src/components/Login'
import Rules from '../src/components/Rules'
import SentenceTask from '../src/components/SentenceTask'
import WordMapTask from '../src/components/WordMap'
import User from '../src/components/User'
import AdminRule from '../src/components/AdminRule'
import AdminSentence from '../src/components/AdminSentence'
import AdminWord from '../src/components/AdminWord'
import AdminUser from '../src/components/AdminUser'

const routes = [
    { path: '/', component: Login }
    , { path: '/user', component: User }
    , { path: '/rules', component: Rules }
    , { path: '/sentencetask', component: SentenceTask }
    , { path: '/wordmap', component: WordMapTask }
    , { path: '/admin/rules', component: AdminRule }
    , { path: '/admin/sentences', component: AdminSentence }
    , { path: '/admin/words', component: AdminWord }
    , { path: '/admin/users', component: AdminUser }
]

export default routes