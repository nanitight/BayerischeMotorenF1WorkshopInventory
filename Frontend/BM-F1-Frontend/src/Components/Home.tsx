import { Link } from 'react-router-dom'

export default function Home() {
  return (
    <div>
      <div
        className="hero min-h-screen"
        style={{
          backgroundImage:
            "url(https://i.ytimg.com/vi/S7a6ymTlGsk/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLB3e4YXZPTcPpJadF63Ln7A95NbqQ)",
        }}
      >
        <div className="hero-overlay"></div>
        <div className="hero-content text-neutral-content text-center">
          <div className="max-w-md">
            <h1 className="mb-5 text-7xl font-bold">Hello there</h1>
            <p className="mb-1 text-3xl">
              Bayerische Motoren F1 Team aims to win the F1 championship someday. 
            </p>
            
            <p className="mb-1">
              Through everyday racing, we aim to improve, and learn from mistakes... 
            </p>
            <Link to="/dashboard" className="btn btn-primary">View Results</Link>
          </div>
        </div>
      </div>

    </div>
  )
}
