import { useState } from 'react'


export default function useApiRequester() {
    const [loading, setLoading] = useState(false) ;
    const loadingScreen = loading ?  <div className="flex items-center justify-center h-screen">
                                <span className="loading loading-infinity loading-xl"></span>
                            </div>
                            : <></> ;
  return {
    loading, 
    setLoading,
    loadingScreen
  }
}
