import "../styles/Nav.css";
import { Link } from 'react-router-dom';

export default function Nav() {
    return (
        <nav className="navbar">
            <h1 className="nav-logo">BlogPlatform</h1>
            <ul className="nav-links">
                <li><Link to="/">Home</Link></li>
                <li><Link to="/login">Login</Link></li>
                <li><Link to="/create-account">Create Account</Link></li>
                <li><Link to="/profile">Profile</Link></li>
            </ul>
        </nav>
    )
}