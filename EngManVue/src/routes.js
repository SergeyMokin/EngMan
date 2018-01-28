import Login from '../src/components/Login'
import Rules from '../src/components/Rules'
import SentenceTask from '../src/components/SentenceTask'
import WordMapTask from '../src/components/WordMap'
import AdminRule from '../src/components/AdminRule'
import AdminSentence from '../src/components/AdminSentence'
import AdminWord from '../src/components/AdminWord'

const routes = [
    { path: '/', component: Login }
    , { path: '/rules', component: Rules }
    , { path: '/sentencetask', component: SentenceTask }
    , { path: '/wordmap', component: WordMapTask }
    , { path: '/admin/rules', component: AdminRule }
    , { path: '/admin/sentences', component: AdminSentence }
    , { path: '/admin/words', component: AdminWord }
]

export default routes