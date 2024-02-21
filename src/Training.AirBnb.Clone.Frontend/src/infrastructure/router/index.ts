import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
      {
          path: '/',
          name: 'Home',
          component: () => import('../../common/views/HomeView.vue')
      },
      {
          path: '/help',
          name: 'Help',
            component: () => import('../../common/views/HelpView.vue')
      },
      {
          path: '/giftcards',
          name: 'GiftCards',
          component: () => import('../../common/views/Giftcards.vue')
      },
      {
          path: '/host/homes',
          name: 'HostHomes',
          component: () => import('../../common/views/HostHomes.vue')
      },
      {
          path:'/listings/:listingId',
          name:'listingDetails',
          component: () => import('../../common/views/ListingDetailsView.vue')
      }
  ]
})

export default router
